using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;

public class PrivateListing : Listing
{
    public PrivateListing(string listingName, string description, Category category, Subcategory subcategory, Location listingLocation, ListingTypeEnum listingType) : base(listingName, description, category, subcategory, listingLocation, ListingTypeEnum.Private)
    {
    }
}