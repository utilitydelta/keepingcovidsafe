using System;

namespace KeepingCovidSafe.Dto
{
    public class Contact
    {
        public string VenueName { get; set; }
        public string VenueAddress { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public double TimeDifferenceInMinutes { get; set; }
    }
}
