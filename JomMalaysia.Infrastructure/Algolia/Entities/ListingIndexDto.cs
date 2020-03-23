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

        public CategoryIndexDto Category { get; set; }
        public string CategoryType { get; set; }

        public ICollection<string> _tags { get; set; }
        public GeoIndexDto _geoloc { get; set; }
    }
    public class CategoryIndexDto
    {
        //include category type
       public List<string> En;
        
       public List<string> Ms;
        
       public List<string> Zh;
    }
    public class AddressIndexDto
    {
       public string Add1 { get; set; }

       public string Add2 { get; set; }
    }

    public class GeoIndexDto
    {
        public double lat { get; set; }

        public double lng { get; set; }
    }

   

    public class MerchantIndexDto
    {
        public string SsmId { get; set; }
        public string RegistrationName { get; set; }
    }
}