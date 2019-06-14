using System.Collections.Generic;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class ListingDto
    {
        public string ListingId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string ListingType { get; set; }
        public AddressDto ListingAddress { get; set; }
        public MerchantDto Merchant { get; set; }
        public string Category { get; set; }
        public ICollection<string> Tags { get; set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }
        public bool isPublished { get; set; }
        
    }
}