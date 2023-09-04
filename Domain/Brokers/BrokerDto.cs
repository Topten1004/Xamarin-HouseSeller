using Immowert4You.Domain.Regions;
using System.Collections.Generic;

namespace Immowert4You.Domain.Brokers
{
    public class BrokerDto
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string[] ZipCodes { get; set; }
        public int Points { get; set; }
        public List<RegionPlaceDto> RegionsPlaces { get; set; }
        public int Place { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsActive { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string ResponsibilityArea { get; set; }
        public string Price { get; set; }
        public string Note { get; set; }
        public List<RegionDto> Regions { get; set; }
        public string PhoneNumber { get; set; }
    }
}
