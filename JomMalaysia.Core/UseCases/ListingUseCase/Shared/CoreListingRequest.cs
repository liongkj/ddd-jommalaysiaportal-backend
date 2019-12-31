using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings.Governments;
using JomMalaysia.Core.Domain.Entities.Listings.Professionals;
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
        public Description Description { get; set; }

        public CategoryType CategoryType { get; set; }

        public string CategoryId { get; set; }
        public List<string> Tags { get; set; }
        public AddressRequest Address { get; set; }
        public List<StoreTimesRequest> OperatingHours { get; set; }
        public OfficialContactRequest Contact { get; set; }
        public List<Service> ProvidedServices { get; set; }
        public List<GovDepartment> Departments { get; set; }
        public ListingImagesRequest ListingImages { get; set; }
        public OfficialContact OfficialContact { get; set; }



    }


}
