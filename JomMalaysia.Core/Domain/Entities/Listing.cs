using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Listing
    {
        public string ListingId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string ListingType { get; set; }
        public Address ListingAddress { get; set; }
        public Merchant Merchant { get; set; }
        public Category Category { get; set; }
        public ICollection<string> Tags { get; private set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }
        public Publish Publish { get; set; }
        public Listing()
        {
            Tags = new Collection<string>();
        }
        
    }
}
