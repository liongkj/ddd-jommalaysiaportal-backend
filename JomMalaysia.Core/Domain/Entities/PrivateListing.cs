using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

public class PrivateListing : Listing
{
    public PrivateListing(string listingName, CategoryPath category, Location listingLocation) : base(listingName, category, listingLocation, ListingTypeEnum.Private)
    {
    }
    public PrivateListing()
    {

    }
}