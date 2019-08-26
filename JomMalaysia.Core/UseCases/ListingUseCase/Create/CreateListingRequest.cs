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
        public CreateListingRequest()
        {

        }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string ListingType { get; set; }


        public string Category { get; set; }
        public string Subcategory { get; set; }

        public List<string> Tags { get; set; }
        public Address Address { get; set; }
        // public string Add1 { get; set; }
        // public string Add2 { get; set; }
        // public string City { get; set; }
        // public string State { get; set; }
        // public string PostalCode { get; set; }
        // public string Country { get; set; }
        public List<List<List<double>>> Coordinates { get; set; }
        public ListingImages ImageUris { get; set; }
        public DateTime EventStartDateTime { get; set; }
        public DateTime EventEndDateTime { get; set; }


    }
}
