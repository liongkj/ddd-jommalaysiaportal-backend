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
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public Address Address { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<string> ListingIds { get; set; }
        public MerchantDto()
        {
            ListingIds = new Collection<string>();
            Contacts = new Collection<Contact>();
        }
    }
}
