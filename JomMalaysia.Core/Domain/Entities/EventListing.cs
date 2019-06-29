using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public sealed class EventListing : Listing
    {
        public DateTime EventDateTime { get; private set; }

        public EventListing(string eventName, string description, Category category, Subcategory subcategory, Location listingLocation, DateTime eventDateTime) : base(eventName, description, category, subcategory, listingLocation, ListingTypeEnum.Event)
        {
            EventDateTime = eventDateTime;

        }

        public void updateEventDate(DateTime eventDateTime)
        {
            EventDateTime = eventDateTime;
        }


    }
}
