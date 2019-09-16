using System;
using JomMalaysia.Infrastructure.Auth0.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows
{
    public class WorkflowSummaryDto
    {
        public UserDto Responder { get; set; }
        public int Lvl { get; set; }
        [BsonIgnoreIfNull]
        public string Comments { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }
    }
}