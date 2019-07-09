using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class ListingDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public int ListingTypeId { get; set; }
        public Address ListingAddress { get; set; }

        public CategoryDto Category { get; set; }

        public ICollection<string> Tags { get; set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }
        public bool isPublished { get; set; }

    }
}