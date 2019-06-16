using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class ListingDto
    {
        public string ListingId { get; set; }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string ListingType { get; set; }
        public Address ListingAddress { get; set; }
        
        public string Category { get; set; }
        public ICollection<string> Tags { get; set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }
        public bool isPublished { get; set; }
        
    }
}