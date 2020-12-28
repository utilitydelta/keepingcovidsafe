using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KeepingCovidSafe.Data;
using Microsoft.AspNetCore.Authorization;

namespace KeepingCovidSafe.Pages.Visit
{
    [AllowAnonymous]
    public class CreateModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public CreateModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(Guid venueId)
        {
            ViewData["VenueId"] = new SelectList(_context.Venues, "Id", "Id");
            Visit = new Data.Visit { VenueId = venueId };
            Venue = _context.Venues.Find(venueId);
            if (Venue == null) return Page();

            var venueManager = _context.VenueManagers.Find(Venue.OwnerEmail);
            if (venueManager == null)
            {
                Venue = null;
            }

            return Page();

        }

        [BindProperty]
        public Data.Visit Visit { get; set; }

        public Data.Venue Venue { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var venue = _context.Venues.Find(Visit.VenueId);

            Visit.VisitTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(venue.TimeZone));

            _context.Visits.Add(Visit);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Visit/Thanks", new {VenueId = Visit.VenueId });
        }
    }
}
