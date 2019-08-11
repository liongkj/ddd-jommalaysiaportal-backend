using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateListingRequest : IUseCaseRequest<CreateListingResponse>
    {
        public CreateListingRequest(string MerchantId, Listing Listing)
        {
            //string ListingName, Category Category, Location Location, ListingTypeEnum listingType, string Description = ""
            Tags = new Collection<string>();
            this.MerchantId = MerchantId;
            ListingName = Listing.ListingName;
            Category = Listing.Category;
            Location = Listing.ListingLocation;
            Description = Listing.Description;

            //put basic info
            NewListing = ListingFactory.CreateListing(Listing.ListingType);

        }

        public Listing NewListing { get; private set; }

        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }

        public DateTime EventDate { get; set; }


        public CategoryPath Category { get; set; }


        public ICollection<string> Tags { get; private set; }
        public Location Location { get; set; }


    }
}
