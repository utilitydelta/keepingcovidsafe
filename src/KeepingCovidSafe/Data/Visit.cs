using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeepingCovidSafe.Data
{
    public class Visit
    {
        public Guid Id { get; set; }

        [DisplayName("Venue")]
        public Guid VenueId { get; set; }

        [DisplayName("Venue")]
        public Venue Venue { get; set; }

        [Required]
        [DisplayName("Visit Time")]
        public DateTime VisitTime { get; set; }

        [Required]
        [MinLength(8)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        [MinLength(2)]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [MinLength(4)]
        [DisplayName("Email")]
        public string Email { get; set; }
    }
}
