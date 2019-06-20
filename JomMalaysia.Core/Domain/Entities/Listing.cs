using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Listing
    {
        private string MerchantId;

        public string ListingId { get; set; }
        public Merchant Merchant { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public ICollection<string> Tags { get; private set; }
        public Location ListingLocation { get; set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }


        public PublishStatus isPublish { get; set; }
        public ListingTypeEnum ListingType { get; private set; }
        public Category Category { get; set; }
        
        
        public Listing()
        {
            Tags = new Collection<string>();
        }

        public Listing(string merchantId, string listingName, string description, Category category, Location listingLocation)
        {
            this.MerchantId = merchantId;
            ListingName = listingName;
            Description = description;
            Category = category;
            ListingLocation = listingLocation;
            Tags = new Collection<string>();
        }

        public void Publish()
        {
            
        }

        public void UpdateCategory(Category category) { }
        public void UpdatePhoto() { }
        public void RemovePhoto() { }
        public void UpdateDescription() { }

        public void UpdateKeywords(Collection<string> tags)
        {

        }

    }
}
