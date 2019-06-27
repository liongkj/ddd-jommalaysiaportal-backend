using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;

public class GovernmentListing : Listing
{
    public string department { get; set; }
    public GovernmentListing(string listingName, string description, Category category, Subcategory subcategory, Location listingLocation, ListingTypeEnum listingType) : base(listingName, description, category, subcategory, listingLocation, ListingTypeEnum.Government)
    {

    }
}