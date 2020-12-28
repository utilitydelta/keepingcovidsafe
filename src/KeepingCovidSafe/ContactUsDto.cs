using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KeepingCovidSafe
{
    public class ContactUsDto
    {
        [DisplayName("Your Email Address")]
        [Required]
        [MinLength(3)]
        public string EmailAddress { get; set; }

        [DisplayName("Message")]
        [Required]
        [MinLength(3)]
        public string Message { get; set; }
    }
}
