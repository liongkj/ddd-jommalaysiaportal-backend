using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings.Attractions
{
    public class Attraction : Listing
    {

        public Attraction()
        {

        }
        public Attraction(CoreListingRequest listing, CategoryPath category, Address address, Merchant merchant) : 
            base(listing.ListingName, merchant, CategoryType.Attraction, category, listing.ListingImages, listing.Tags, listing.Description, address, listing.OperatingHours, listing.OfficialContact)
        {

        }
    }
}