using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings.Attractions;
using JomMalaysia.Core.Domain.Entities.Listings.Governments;
using JomMalaysia.Core.Domain.Entities.Listings.Professionals;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Factories
{
    public static class ListingFactory
    {
  
        public static Listing CreateListing(ListingTypeEnum listingType, CoreListingRequest listing, Category category, Merchant merchant)
        { 
            var listingTypeId = listingType.Id;
            var type =  EnumerationBase.Parse<ListingTypeEnum>(listingTypeId);

            var categoryPath = category?.CategoryPath;

            if(type.Equals(ListingTypeEnum.Attraction)) return new Attraction(listing, categoryPath,GenerateAddress(listing), merchant);
            if(type.Equals(ListingTypeEnum.ProfessionalService)) return new ProfessionalService(listing, categoryPath,GenerateAddress(listing), merchant);
            if(type.Equals(ListingTypeEnum.GovernmentOrg)) return new GovernmentOrg(listing, categoryPath,GenerateAddress(listing), merchant);
            if(type.Equals(ListingTypeEnum.NonProfitOrg)) return new NonProfitOrg(listing, categoryPath,GenerateAddress(listing), merchant);
            return type.Equals(ListingTypeEnum.PrivateSector) ? new PrivateSector(listing, categoryPath, GenerateAddress(listing), merchant) : null;
        }


        private static Address GenerateAddress(CoreListingRequest listing)
        {
            var coord = listing.Address.Coordinates;
            var coor = new Coordinates(coord.Longitude, coord.Latitude);

            var address = new Address(listing.Address.Add1, listing.Address.Add2, listing.Address.City, listing.Address.State, listing.Address.PostalCode, listing.Address.Country, coor);
            return address;
        }
    }
}