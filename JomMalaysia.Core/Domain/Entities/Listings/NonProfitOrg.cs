using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    public class NonProfitOrg : Listing
    {

        public NonProfitOrg()
        {

        }
        public NonProfitOrg(CoreListingRequest listing,CategoryPath category,  Address address, Merchant merchant) : base(listing.ListingName, merchant, ListingTypeEnum.NonProfitOrg, category,  listing.ImageUris, listing.Tags, listing.Description, address, listing.OperatingHours)
        {

        }
    }
}