using System;
using System.Collections.Generic;
using System.Text;

namespace Immowert4You.Domain.Partners
{
    public class PartnerDto
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string ContactPerson { get; set; }
        public string PhoneNumber { get; set; }
        public string ZipCode { get; set; }
        public float Price { get; set; }
        public DateTime DurationFrom { get; set; }
        public DateTime DurationTo { get; set; }
        public string WebsiteURL { get; set; }
        public string Profession { get; set; }
        public string Note { get; set; }
        public string Logo { get; set; }
    }
}
