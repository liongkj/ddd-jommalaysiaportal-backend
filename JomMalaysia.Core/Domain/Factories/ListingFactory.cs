using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;

namespace JomMalaysia.Core.Domain.Factories
{
    public static class ListingFactory
    {
        private const int GOV = 1;
        private const int PRI = 2;
        private const int EVE = 3;

        public static Listing CreateListing(ListingTypeEnum ListingType, CreateListingRequest listing)
        {
            int listingTypeId = ListingType.Id;
            
            switch (listingTypeId)
            {
                case EVE:
                    
                    return new EventListing(listing);
                default:
                    return new PrivateListing(listing);
            }
        }
    }
}