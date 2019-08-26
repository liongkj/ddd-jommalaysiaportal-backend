using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;

public class PrivateListing : Listing
{
    public PrivateListing(CreateListingRequest listing, Merchant merchant) : base(listing.ListingName, merchant, new CategoryPath(listing.Category, listing.Subcategory), ListingTypeEnum.Private, listing.ImageUris, listing.Tags, listing.Description, listing.Address)
    {
    }
    public PrivateListing()
    {

    }
}