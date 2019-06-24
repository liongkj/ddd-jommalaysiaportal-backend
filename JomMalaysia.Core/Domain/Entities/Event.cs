using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Event:Listing
    {

        public DateTime EventDateTime { get; set; }

        internal Event(string merchantId, string listingName, string description, Category category, Location listingLocation,DateTime eventDateTime) : base( merchantId,  listingName,  description,  category,  listingLocation)
        {
            ListingType = ListingTypeEnum.Event;
            EventDateTime = eventDateTime;
        }

        public static Event CreateEvent(string merchantId, string listingName, string description, Category category, Location listingLocation,DateTime eventDateTime)
        {
            return new Event(merchantId, listingName, description, category, listingLocation,eventDateTime);
        }

    }
}
