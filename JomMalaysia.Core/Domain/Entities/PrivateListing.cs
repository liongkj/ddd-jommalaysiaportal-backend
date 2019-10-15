using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

public class PrivateListing : Listing
{
    public PrivateListing(CoreListingRequest listing, Category category, Address address, Merchant merchant) : base(listing.ListingName, merchant, category.CategoryPath, ListingTypeEnum.Private, listing.ImageUris, listing.Tags, listing.Description, address)
    {
    }
    public PrivateListing()
    {

    }
}