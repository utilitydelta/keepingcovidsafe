using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KeepingCovidSafe.Data;

namespace KeepingCovidSafe.Pages.Venue
{
    public class DeleteModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public DeleteModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Venue Venue { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venue = await _context.Venues.FirstOrDefaultAsync(m => m.Id == id);

            if (Venue == null || Venue.OwnerEmail != User.Identity.Name)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venue = await _context.Venues.FindAsync(id);

            if (Venue == null || Venue.OwnerEmail != User.Identity.Name)
            {
                return RedirectToPage("./Index");
            }

            _context.Venues.Remove(Venue);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
