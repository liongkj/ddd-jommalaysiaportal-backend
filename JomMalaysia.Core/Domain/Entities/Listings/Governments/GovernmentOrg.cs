using System.Collections.Generic;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings.Governments
{
    public class GovernmentOrg : Listing
    {
        public List<GovDepartment> Departments { get; set; }
        public GovernmentOrg()
        {

        }
        public GovernmentOrg(CoreListingRequest listing, CategoryPath category, Address address, Merchant merchant, OfficialContact officialContact) : base(listing.ListingName, merchant, CategoryType.Government, category, listing.ListingImages, listing.Tags, listing.Description, address, listing.OperatingHours, officialContact, listing.PublishStatus, listing.IsFeatured)
        {
            Departments = listing.Departments;
        }
    }
}