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
        public CompanyRegistrationNumber CompanyRegistrationNumber { get; set; }
        public Address Address { get; set; }
        public List<ContactsDto> Contacts { get; set; }
        public List<string> ListingIds { get; set; }
        public MerchantDto()
        {
            ListingIds = new List<string>();
            Contacts = new List<ContactsDto>();
        }
    }
}
