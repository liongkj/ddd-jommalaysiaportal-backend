using System.Collections.Generic;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows
{
    public class ListingSummaryDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public MerchantSummaryDto Merchant { get; set; }
        public string ListingName { get; set; }
        public string ListingType { get; set; }
        public string Category { get; set; }
        public ICollection<string> Tags { get; set; }
        public string Status { get; set; }

        [BsonIgnoreIfNull]
        public BsonDateTime CreatedAt { get; set; }
        [BsonIgnoreIfNull]
        public BsonDateTime ModifiedAt { get; set; }
    }
}
