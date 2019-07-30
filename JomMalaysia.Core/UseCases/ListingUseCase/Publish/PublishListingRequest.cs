using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Publish
{
    public class PublishListingRequest : IUseCaseRequest<PublishListingResponse>
    {
        public string UserId { get; set; }
        public List<string> ListingIds { get; set; }


        public PublishListingRequest(List<string> listingIds, string UserId)
        {
            ListingIds = listingIds;
            this.UserId = UserId;
        }

        public PublishListingRequest(List<string> listingId)
        {
            ListingIds = listingId;
        }
    }
}
