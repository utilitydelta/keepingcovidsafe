using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeepingCovidSafe.Data
{
    public class Venue
    {
        public Guid Id { get; set; }

        [DisplayName("Venue Address")]
        [Required]
        [MinLength(3)]
        public string Address { get; set; }

        [DisplayName("Email Address")]
        public string OwnerEmail { get; set; }

        [DisplayName("Venue Name")]
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [DisplayName("Instagram Link")]
        public string InstagramLink { get; set; }

        [DisplayName("Time Zone")]
        [Required]
        public string TimeZone { get; set; }
    }
}
