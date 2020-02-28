using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows
{
    public class WorkflowDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string WorkflowId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public ListingSummaryDto Listing { get; set; }
        public UserDtoSummary Requester { get; set; }
        public UserDtoSummary Responder { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }
        [BsonIgnoreIfNull]
        public List<WorkflowSummaryDto> HistoryData { get; set; }
    }
}
