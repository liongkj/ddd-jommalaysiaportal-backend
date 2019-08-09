using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateListingRequest : IUseCaseRequest<CreateListingResponse>
    {
        public CreateListingRequest(string MerchantId, Listing listing)
        {

            this.MerchantId = MerchantId;
            this.ListingName = listing.ListingName;
            this.Description = listing.Description;
            this.Category = listing.Category;
            this.ListingLocation = listing.ListingLocation;
            ListingType = listing.ListingType;
        }

        private Listing ListingMapper(ListingTypeEnum type)
        {
            switch (type):
            case    
        }

        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }

        public ListingTypeEnum ListingType { get; set; }
        public Category Category { get; set; }
        public Location ListingLocation { get; set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }

    }
}
