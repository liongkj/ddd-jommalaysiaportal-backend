using System;
using JomMalaysia.Infrastructure.Auth0.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows
{
    public class WorkflowSummaryDto
    {
        public UserDtoSummary Responder { get; set; }
        public string Status { get; set; }
        [BsonIgnoreIfNull]
        public string Comments { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }
    }
}