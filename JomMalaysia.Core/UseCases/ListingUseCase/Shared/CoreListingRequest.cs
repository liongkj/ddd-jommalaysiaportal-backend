using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Shared
{
    public class CoreListingRequest : IUseCaseRequest<CoreListingResponse>, IUseCaseRequest<UpdateListingResponse>
    {
        public CoreListingRequest()
        {

        }
        public string ListingId { get; set; }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }

        public string ListingType { get; set; }

        public string CategoryId { get; set; }
        public List<string> Tags { get; set; }
        public AddressRequest Address { get; set; }
        public List<StoreTimesRequest> OperatingHours { get; set; }


        public ListingImages ImageUris { get; set; }
        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }


    }
}
