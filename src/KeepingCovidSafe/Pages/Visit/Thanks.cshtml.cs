using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KeepingCovidSafe.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KeepingCovidSafe.Pages.Visit
{
    [AllowAnonymous]
    public class ThanksModel : PageModel
    {
        private readonly KeepingCovidSafe.Data.ApplicationDbContext _context;

        public ThanksModel(KeepingCovidSafe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Data.Venue Venue;
        public void OnGet(Guid venueId)
        {
            Venue = _context.Venues.Find(venueId);
        }
    }
}
