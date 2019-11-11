using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows
{
    public class ListingSummaryDto : IListingDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string ListingId { get; set; }
        public WorkflowMerchantSummaryDto Merchant { get; set; }
        public string ListingName { get; set; }
        public string ListingType { get; set; }
        public string Status { get; set; }
    }
}
