using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Factories
{
    public static class ListingFactory
    {
        // Refer ListingTypeEnum
        private const int GOV = 2;
        private const int PRI = 1;
        private const int EVE = 3;
        private const int SOC = 4;

        public static Listing CreateListing(ListingTypeEnum ListingType, CoreListingRequest listing, Merchant merchant)
        {
            int listingTypeId = ListingType.Id;
            List<Coordinates> cor = new List<Coordinates>();
            foreach (var c in listing.Coordinates)
                foreach (var b in c)
                {
                    cor.Add(new Coordinates(b[0], b[1]));
                }
            var Address = new Address(listing.Address.Add1, listing.Address.Add2, listing.Address.City, listing.Address.State, listing.Address.PostalCode, listing.Address.Country, cor);

            switch (listingTypeId)
            {
                case EVE:
                    return new EventListing(listing, Address, merchant);
                case PRI:
                    return new PrivateListing(listing, Address, merchant);
                default:
                    return null;
            }
        }
    }
}