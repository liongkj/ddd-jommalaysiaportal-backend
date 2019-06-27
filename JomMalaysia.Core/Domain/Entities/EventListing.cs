using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class EventListing : Listing
    {

        public DateTime EventDateTime { get; set; }

        public EventListing()
        {

        }
        public EventListing(string listingName, string description, Category category, Location listingLocation, DateTime eventDateTime)
        {
            ListingType = ListingTypeEnum.Event;
            EventDateTime = eventDateTime;
        }
        public void setDetails(DateTime eventDateTime)
        {
            EventDateTime = eventDateTime;
        }
    }
}
