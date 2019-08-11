using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Api.Serialization
{
    public abstract class BaseListingHolder
    { 
        public string Id { get; set; }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string ListingType { get; set; }
        public Address ListingAddress { get; set; }

        public string Category { get; set; }
        public string Subcategory { get; set; }

        public ICollection<string> Tags { get; set; }
        public ListingImages ListingImages { get; set; }
        
        public string Status { get; set; }
        public Contact Contact { get; internal set; }
    }
}