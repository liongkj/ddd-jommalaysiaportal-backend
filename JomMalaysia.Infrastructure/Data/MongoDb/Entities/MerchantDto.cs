using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class MerchantDto
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public Name ContactName { get; set; }
        public Address Address { get; set; }
        public Email ContactEmail { get; set; }
        public Phone Phone { get; set; }
        public string Fax { get; set; }
        public ICollection<ListingDto> Listings { get; set; }
        public MerchantDto()
        {
            Listings = new Collection<ListingDto>();
        }
    }
}
