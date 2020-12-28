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
    public class DetailsModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public DetailsModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Venue Venue { get; set; }
        public string BaseUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Venue = await _context.Venues.FirstOrDefaultAsync(m => m.Id == id);
            BaseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            if (Venue == null || Venue.OwnerEmail != User.Identity.Name)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
