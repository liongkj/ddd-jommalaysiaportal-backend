using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Infrastructure.Data.MongoDb.Interfaces;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class ListingDto : IEntityDateTime
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string MerchantId { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public string ListingType { get; set; }
        public Address ListingAddress { get; set; }

        public string Category { get; set; }

        public ICollection<string> Tags { get; set; }
        public ListingImages ListingImages { get; set; }

        public string Status { get; set; }
        [BsonIgnoreIfNull]
        public BsonDateTime EventStartDateTime { get; set; }
        [BsonIgnoreIfNull]
        public BsonDateTime EventEndDateTime { get; set; }
        public BsonDateTime CreatedAt { get; set; }
        [BsonIgnoreIfNull]
        public BsonDateTime ModifiedAt { get; set; }
    }
}