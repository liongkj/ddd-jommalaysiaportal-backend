using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    public class LocalListing : Listing, IWithCategory
    {
        public CategoryPath Category { get; set; }
        public LocalListing(CoreListingRequest listing, CategoryPath category, Address address, Merchant merchant) : base(listing.ListingName, merchant, ListingTypeEnum.Local, listing.ImageUris, listing.Tags, listing.Description, address, listing.OperatingHours)
        {
            Category = category;
        }
        public LocalListing()
        {

        }

    }
}