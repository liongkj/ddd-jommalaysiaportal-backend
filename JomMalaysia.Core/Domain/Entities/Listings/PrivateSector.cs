﻿using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    public sealed class PrivateSector : Listing
    {
      
        public PrivateSector()
        {

        }
        
        public PrivateSector(CoreListingRequest listing, CategoryPath category, Address address, Merchant merchant) : base(listing.ListingName, merchant, ListingTypeEnum.PrivateSector,  category, listing.ImageUris, listing.Tags, listing.Description, address, listing.OperatingHours)
        {
         
        }
     




    }
}
