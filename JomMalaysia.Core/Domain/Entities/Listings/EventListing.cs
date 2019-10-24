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
    public sealed class EventListing : Listing
    {

        public EventListing()
        {

        }
        public EventListing(CoreListingRequest listing, Category category, Address address, Merchant merchant) : base(listing.ListingName, merchant, category.CategoryPath, ListingTypeEnum.Event, listing.ImageUris, listing.Tags, listing.Description, address)
        {
            EventStartDateTime = listing.EventStartDateTime;
            EventEndDateTime = listing.EventEndDateTime;
        }
        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }

        public void UpdateEventDate(DateTime EventStartDateTime, DateTime EventEndDateTime)
        {
            this.EventStartDateTime = EventStartDateTime;
            this.EventEndDateTime = EventEndDateTime;
        }


    }
}
