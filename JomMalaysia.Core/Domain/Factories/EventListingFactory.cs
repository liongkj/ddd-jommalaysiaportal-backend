using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Factories
{
    public class EventListingFactory : Factory
    {

        public EventListingFactory()
        {
        }

        public override Listing CreateListing(string listingName, Category category,  Location listingLocation, ListingTypeEnum ListingTypeEnum)
        {
            return new EventListing(listingName, category, listingLocation, ListingTypeEnum);
        }
    }
}