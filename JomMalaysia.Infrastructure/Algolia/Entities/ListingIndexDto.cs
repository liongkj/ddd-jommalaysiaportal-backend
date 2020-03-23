using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.SharedRequest;
using MongoDB.Bson;

namespace JomMalaysia.Infrastructure.Algolia.Entities
{
    public class ListingIndexDto
    {
        public string ObjectID { get; set; }
        public MerchantIndexDto Merchant { get; set; }
        public string ListingName { get; set; }
        public string Photo { get; set; }
        public Description Description { get; set; }
        public AddressIndexDto Address { get; set; }

        public ListingViewModel.CategoryPathViewModel Category { get; set; }
        public string CategoryType { get; set; }

        public ICollection<string> Tags { get; set; }
        public GeoIndexDto _geoloc { get; set; }
    }
    
    public class AddressIndexDto
    {
        string Add1 { get; set; }

        string Add2 { get; set; }
    }

    public class GeoIndexDto
    {
        public double lat { get; set; }

        public double lng { get; set; }
    }

    public class CategoryIndexDto
    {
        //TODO
        // "media_category": "tv series",                 // string attribute for filtering
        // "subject_category": ["drugs", "divorced dad"], 
    }

    public class MerchantIndexDto
    {
        public string SsmId { get; set; }
        public string RegistrationName { get; set; }
    }
}