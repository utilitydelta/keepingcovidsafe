using KeepingCovidSafe.Dto;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace KeepingCovidSafe.Cli
{
    //https://docs.microsoft.com/en-us/aspnet/web-api/overview/advanced/calling-a-web-api-from-a-net-client
    class Program
    {
        static HttpClient client = new HttpClient();

        static void Main(string[] args)
        {
            Console.WriteLine("Enter your API key (guid):");
            var apiKey = new Guid(Console.ReadLine());

            Console.WriteLine("1. Add visit");
            Console.WriteLine("2. Trace contact");
            var option = Console.ReadLine();

            client.BaseAddress = new Uri("https://keepingcovidsafe.com");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (option == "1")
            {
                Console.WriteLine("Enter Venue Id (Guid):");
                var venueId = new Guid(Console.ReadLine());

                Console.WriteLine("Enter person's name:");
                var name = Console.ReadLine();

                Console.WriteLine("Enter Phone Number:");
                var phoneNumber = Console.ReadLine();

                var visitDto = new Dto.VisitDto
                {
                    VenueId = venueId,
                    ApiKey = apiKey,
                    Name = name,
                    PhoneNumber = phoneNumber
                };

                var response = client.PostAsJsonAsync("api/visits", visitDto).GetAwaiter().GetResult();
                Console.WriteLine("Result: " + response.StatusCode);

            } else if (option == "2")
            {
                Console.WriteLine("Enter Phone Number:");
                var phoneNumber = Console.ReadLine();

                var response = client.GetAsync($"api/visits?apiKey={apiKey}&phoneNumber={phoneNumber}").GetAwaiter().GetResult();
                Console.WriteLine("Result: " + response.StatusCode);
                if (response.IsSuccessStatusCode)
                {
                    var contacts = response.Content.ReadAsAsync<Contact[]>().GetAwaiter().GetResult();
                    Console.WriteLine("Name,PhoneNumber,TimeDifferenceInMinutes,VenueAddress,VenueName");
                    foreach (var contact in contacts)
                    {
                        Console.WriteLine($"{contact.Name},{contact.PhoneNumber},{contact.TimeDifferenceInMinutes},{contact.VenueAddress},{contact.VenueName}");
                    }
                }
            }
            Console.WriteLine("Done.");
        }
    }
}
