using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;

public class PrivateListing : Listing
{
    public PrivateListing(CreateListingRequest listing) : base(listing.ListingName, new CategoryPath(listing.Category, listing.Subcategory), ListingTypeEnum.Event,listing.ImageUris, listing.Tags, listing.Description, listing.Address, listing.Coordinates)
    {
    }
    public PrivateListing()
    {

    }
}