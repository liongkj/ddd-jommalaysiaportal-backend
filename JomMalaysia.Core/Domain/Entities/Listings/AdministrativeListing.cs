using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    public class AdministrativeListing : Listing
    {

        public AdministrativeListing()
        {

        }
        public AdministrativeListing(CoreListingRequest listing, Address address, Merchant merchant) : base(listing.ListingName, merchant, ListingTypeEnum.Gover, listing.ImageUris, listing.Tags, listing.Description, address, listing.OperatingHours)
        {

        }
    }
}