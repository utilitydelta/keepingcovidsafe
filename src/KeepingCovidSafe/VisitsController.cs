using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KeepingCovidSafe.Data;
using KeepingCovidSafe.Dto;
using System.Net;

namespace KeepingCovidSafe
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private readonly Guid _apiKey = new Guid("YOUR-API-KEY-HERE");

        public VisitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Visits
        [HttpGet]
        public async Task<ActionResult<List<Contact>>> GetContacts(Guid apiKey, string phoneNumber)
        {
            if (apiKey != _apiKey || string.IsNullOrEmpty(phoneNumber))
            {
                return StatusCode(500);
            }

            var allContacts = new List<Contact>();

            var visitsForInfectedPerson = await _context.Visits.Where(x => x.PhoneNumber == phoneNumber).ToListAsync();
            foreach (var infectedVisit in visitsForInfectedPerson)
            {
                var beforeTime = infectedVisit.VisitTime.Subtract(new TimeSpan(8, 0, 0));
                var afterTime = infectedVisit.VisitTime.Add(new TimeSpan(8, 0, 0));

                var contacts = await _context.Visits.Where(x =>
                    x.PhoneNumber != phoneNumber &&
                    x.VenueId == infectedVisit.VenueId &&
                    x.VisitTime < afterTime && x.VisitTime > beforeTime)
                    .Select(x => new Contact
                    {
                        Name = x.Name,
                        Email = x.Email,
                        PhoneNumber = x.PhoneNumber,
                        VenueName = x.Venue.Name,
                        VenueAddress = x.Venue.Address,
                        TimeDifferenceInMinutes = x.VisitTime.Subtract(infectedVisit.VisitTime).TotalMinutes
                    }).ToListAsync();
                allContacts.AddRange(contacts);
            }

            return allContacts.OrderBy(x => x.TimeDifferenceInMinutes).ToList();
        }

        [HttpPost]
        public async Task<ActionResult> AddVisit(VisitDto visit)
        {
            if (visit.ApiKey != _apiKey || string.IsNullOrEmpty(visit.PhoneNumber))
            {
                return StatusCode(500);
            }

            _context.Visits.Add(new Visit { Name = visit.Name, Email = visit.Email, PhoneNumber = visit.PhoneNumber, VenueId = visit.VenueId, VisitTime = DateTime.UtcNow });
            await _context.SaveChangesAsync();

            return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
