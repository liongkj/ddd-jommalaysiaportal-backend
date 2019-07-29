using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

public class PrivateListing : Listing
{
    public PrivateListing(string listingName, string description, Category category, Location listingLocation, ListingTypeEnum listingType) : base(listingName, description, category, listingLocation, ListingTypeEnum.Private)
    {
    }
}