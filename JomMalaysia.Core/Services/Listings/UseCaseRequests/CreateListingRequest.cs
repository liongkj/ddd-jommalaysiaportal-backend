using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Listings.UseCaseResponses;

namespace JomMalaysia.Core.Services.Listings.UseCaseRequests
{
    public class CreateListingRequest : IUseCaseRequest<CreateListingResponse>
    {
        public CreateListingRequest(string MerchantId, string ListingName, string Description, Category Category, Location Location)
        {
            Tags = new Collection<string>();

            this.MerchantId = MerchantId;
            this.ListingName = ListingName;
            this.Description = Description;
            this.Category = Category;
            this.ListingLocation = Location;
        }
        public CreateListingRequest(string merchantId, Listing l)
        {
            Tags = l.Tags;
            this.MerchantId = merchantId;
            this.ListingName = l.ListingName;
            this.Description = l.Description;
            this.Category = l.Category;
            this.ListingLocation = l.ListingLocation;
        }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }

        public ListingTypeEnum ListingType { get; set; }

        public Category Category { get; set; }

        public ICollection<string> Tags { get; private set; }
        public Location ListingLocation { get; set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }

    }
}
