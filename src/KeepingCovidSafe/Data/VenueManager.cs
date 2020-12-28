using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeepingCovidSafe.Data
{
    public class VenueManager
    {
        [Key]
        [DisplayName("Email Address")]
        public string OwnerEmail { get; set; }
    }
}
