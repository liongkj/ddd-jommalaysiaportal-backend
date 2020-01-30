using System.Collections.Generic;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings.Professionals
{
    public class ProfessionalService : Listing
    {
        public List<Service> ProvidedServices { get; set; }
        public ProfessionalService(CoreListingRequest listing, CategoryPath category, Address address, Merchant merchant, OfficialContact officialContact) : base(listing.ListingName, merchant, CategoryType.Professional, category, listing.ListingImages, listing.Tags, listing.Description, address, listing.OperatingHours, officialContact, listing.PublishStatus, listing.IsFeatured)
        {
            ProvidedServices = listing.ProvidedServices;
        }
        public ProfessionalService()
        {

        }

    }
}