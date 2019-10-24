using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    //https://www.dofactory.com/net/factory-method-design-pattern
    public sealed class CivicListing : Listing
    {
        private CoreListingRequest listing;

        public CivicListing()
        {

        }

        public CivicListing(CoreListingRequest listing, Address address, Merchant merchant)
        {
            this.listing = listing;
            Address = address;
            Merchant = merchant;
        }

        public CivicListing(CoreListingRequest listing, Category category, Address address, Merchant merchant) : base(listing.ListingName, merchant, ListingTypeEnum.Event, listing.ImageUris, listing.Tags, listing.Description, address, listing.OperatingHours)
        {
            OpeningHoursStart = listing.EventStartDateTime;
            OpeningHoursEnd = listing.EventEndDateTime;
        }
        public DateTime OpeningHoursStart { get; set; }
        public DateTime OpeningHoursEnd { get; set; }




    }
}
