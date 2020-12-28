using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KeepingCovidSafe.Data;
using Microsoft.AspNetCore.Identity;

namespace KeepingCovidSafe.Pages.Visit
{
    public class IndexModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public IndexModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Visit> Visit { get;set; }
        public Data.Venue Venue { get;set; }

        public async Task<IActionResult> OnGetAsync(Guid venueId)
        {
            Venue = _context.Venues.Find(venueId);
            if (Venue == null || Venue.OwnerEmail != User.Identity.Name)
            {
                return RedirectToPage("./Index");
            }

            var name = User.Identity.Name;

            Visit = await _context.Visits.Where(x => x.Venue.OwnerEmail == name && x.VenueId == venueId)
                .Include(v => v.Venue).OrderByDescending(x => x.VisitTime).ToListAsync();

            return Page();
        }
    }
}
