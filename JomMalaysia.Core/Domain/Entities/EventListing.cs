using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;

namespace JomMalaysia.Core.Domain.Entities
{
    //https://www.dofactory.com/net/factory-method-design-pattern
    public sealed class EventListing : Listing
    {

        public EventListing()
        {

        }
        public EventListing(CreateListingRequest listing) : base(listing.ListingName, new CategoryPath(listing.Category,listing.Subcategory),ListingTypeEnum.Event, listing.ImageUris, listing.Tags, listing.Description, listing.Address, listing.Coordinates)
        {
            EventDateTime = listing.EventDate;
        }
        public DateTime EventDateTime { get; private set; }


        public void updateEventDate(DateTime eventDateTime)
        {
            EventDateTime = eventDateTime;
        }


    }
}
