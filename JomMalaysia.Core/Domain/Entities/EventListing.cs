using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    //https://www.dofactory.com/net/factory-method-design-pattern
    public sealed class EventListing : Listing
    {
        public EventListing(string listingName, string description, Category category, Location listingLocation, ListingTypeEnum listingType) : base(listingName, description, category, listingLocation, listingType)
        {
        }

        public EventListing()
        {

        }
        public DateTime EventDateTime { get; private set; }


        public EventListing(string eventName, string description, Category category, Location listingLocation, DateTime eventDateTime) : base(eventName, description, category, listingLocation, ListingTypeEnum.Event)
        {
            EventDateTime = eventDateTime;

        }

        public void updateEventDate(DateTime eventDateTime)
        {
            EventDateTime = eventDateTime;
        }


    }
}
