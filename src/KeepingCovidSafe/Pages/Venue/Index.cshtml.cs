using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KeepingCovidSafe.Data;
using Microsoft.AspNetCore.Authorization;

namespace KeepingCovidSafe.Pages.Venue
{
    public class IndexModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public IndexModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Data.Venue> Venue { get;set; }
        public Data.VenueManager VenueManager { get;set; }

        public async Task OnGetAsync()
        {
            var email = User.Identity.Name;

            VenueManager = await _context.VenueManagers.FindAsync(email);
            if (VenueManager == null)
            {
                VenueManager = new VenueManager { OwnerEmail = email };
                _context.VenueManagers.Add(VenueManager);
                await _context.SaveChangesAsync();
            }

            Venue = await _context.Venues.Where(x => x.OwnerEmail == email).ToListAsync();
        }
    }
}
