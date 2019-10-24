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
        public Category Category { get; set; }
        public LocalListing(CoreListingRequest listing, Category category, Address address, Merchant merchant) : base(listing.ListingName, merchant, ListingTypeEnum.Local, listing.ImageUris, listing.Tags, listing.Description, address)
        {
            Category = category;
        }
        public LocalListing()
        {

        }
        public Dictionary<string, string> UpdateCategory(Listing toBeUpdateListings, bool IsUpdateCategoryOperation = true)
        {
            // Dictionary<string, string> UpdatedListings = new Dictionary<string, string>();
            // foreach (var listing in toBeUpdateListings)
            // {
            //     CategoryPath cp;
            //     if (IsUpdateCategoryOperation)
            //     {
            //         cp = new CategoryPath(this.CategoryName, listing.Category.Subcategory);

            //     }
            //     else
            //     {
            //         cp = new CategoryPath(listing.Category.Category, this.CategoryName);
            //     }
            //     UpdatedListings.Add(listing.ListingId, cp.ToString());
            // }
            // return UpdatedListings;
            throw new NotImplementedException();
        }

        public Dictionary<string, string> updateCategory(Listing toBeUpdate, bool IsUpdateCategory = true)
        {
            throw new NotImplementedException();
        }
    }
}