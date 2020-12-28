using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KeepingCovidSafe.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Collections.ObjectModel;

namespace KeepingCovidSafe.Pages.Venue
{
    public class CreateModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public CreateModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            TimeZoneInfos = TimeZoneInfo.GetSystemTimeZones();
            return Page();
        }

        [BindProperty]
        public Data.Venue Venue { get; set; }

        public ReadOnlyCollection<TimeZoneInfo> TimeZoneInfos { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Venue.OwnerEmail = User.Identity.Name;

            var venueCount = await _context.Venues.Where(x => x.OwnerEmail == Venue.OwnerEmail).CountAsync();
            if (venueCount == 10)
            {
                ModelState.AddModelError("", "Cannot add more than 10 venues per account.");
                return Page();
            }

            _context.Venues.Add(Venue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
