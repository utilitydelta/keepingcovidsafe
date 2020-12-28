using System;
using System.Collections.Generic;
using System.Text;

namespace KeepingCovidSafe.Dto
{
    public class VisitDto
    {
        public Guid ApiKey { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public Guid VenueId { get; set; }
    }
}
