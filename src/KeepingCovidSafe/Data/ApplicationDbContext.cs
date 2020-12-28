using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KeepingCovidSafe.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Venue> Venues { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<VenueManager> VenueManagers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
