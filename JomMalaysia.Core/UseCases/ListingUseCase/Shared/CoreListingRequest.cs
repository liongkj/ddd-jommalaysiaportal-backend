using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.SharedRequest;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Shared
{
    public class CoreListingRequest : IUseCaseRequest<CoreListingResponse>, IUseCaseRequest<CreateWorkflowResponse>
    {
        public CoreListingRequest()
        {

        }
        public string ListingId { get; set; }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string ListingType { get; set; }


        public string Category { get; set; }
        public string Subcategory { get; set; }

        public List<string> Tags { get; set; }
        public AddressRequest Address { get; set; }

        public List<List<List<double>>> Coordinates { get; set; }
        public ListingImages ImageUris { get; set; }
        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }


    }
}
