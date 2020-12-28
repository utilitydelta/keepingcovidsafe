using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KeepingCovidSafe.Data;
using System.Collections.ObjectModel;

namespace KeepingCovidSafe.Pages.Venue
{
    public class EditModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public EditModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Venue Venue { get; set; }

        public ReadOnlyCollection<TimeZoneInfo> TimeZoneInfos { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            TimeZoneInfos = TimeZoneInfo.GetSystemTimeZones();

            Venue = await _context.Venues.FirstOrDefaultAsync(m => m.Id == id);

            if (Venue == null || Venue.OwnerEmail != User.Identity.Name)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var dbVenue = _context.Venues.Find(Venue.Id);
            if (dbVenue == null || dbVenue.OwnerEmail != User.Identity.Name)
            {
                return NotFound();
            }

            dbVenue.Name = Venue.Name;
            dbVenue.InstagramLink = Venue.InstagramLink;
            dbVenue.Address = Venue.Address;
            dbVenue.TimeZone = Venue.TimeZone;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VenueExists(Venue.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VenueExists(Guid id)
        {
            return _context.Venues.Any(e => e.Id == id);
        }
    }
}
