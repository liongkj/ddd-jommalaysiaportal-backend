using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities.Listings;
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
        private const int CIVIC = 2;
        private const int LOCAL = 1;
        private const int EVENT = 3;
        private const int GOVER = 4;

        public static Listing CreateListing(ListingTypeEnum ListingType, CoreListingRequest listing, Category category, Merchant merchant)
        {
            int listingTypeId = ListingType.Id;

            switch (listingTypeId)
            {
                case EVENT:
                    return new EventListing(listing, category, GenerateAddress(listing), merchant);
                case LOCAL:
                    return new LocalListing(listing, category, GenerateAddress(listing), merchant);
                case CIVIC:
                    return new CivicListing(listing, GenerateAddress(listing), merchant);
                case GOVER:
                    return new AdministrativeListing(listing, GenerateAddress(listing), merchant);
                default:
                    return null;
            }
        }


        private static Address GenerateAddress(CoreListingRequest listing)
        {
            List<Coordinates> cor = new List<Coordinates>();
            foreach (var c in listing.Coordinates)
                foreach (var b in c)
                {
                    cor.Add(new Coordinates(b[0], b[1]));
                }
            var Address = new Address(listing.Address.Add1, listing.Address.Add2, listing.Address.City, listing.Address.State, listing.Address.PostalCode, listing.Address.Country, cor);
            return Address;
        }
    }
}