using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Factories
{
    public static class ListingFactory
    {
        private const int GOV = 1;
        private const int PRI = 2;
        private const int EVE = 3;

        public static Listing Create(string listingName, Category category, Location listingLocation, ListingTypeEnum ListingTypeEnum)
        {
            int listingTypeId = ListingTypeEnum.Id;
            switch (listingTypeId)
            {
                case EVE:
                    return new EventListingFactory()
                        .CreateListing(listingName, category,  listingLocation, ListingTypeEnum);
                default:
                    return new EventListingFactory()
                        .CreateListing(listingName, category, listingLocation, ListingTypeEnum);
            }
        }
    }
}