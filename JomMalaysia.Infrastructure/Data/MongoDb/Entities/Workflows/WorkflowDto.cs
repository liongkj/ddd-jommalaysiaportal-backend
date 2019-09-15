using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Infrastructure.Auth0.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities.Workflows
{
    public class WorkflowDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Type { get; set; }
        public int Lvl { get; set; }
        public string Merchant { get; set; }

        public ListingSummaryDto Listing { get; set; }
        public UserDto Requester { get; set; }
        [BsonIgnoreIfNull]
        public UserDto Responder { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime Created { get; set; }
        [BsonIgnoreIfNull]
        public ICollection<WorkflowDto> PreviousWorkflows { get; set; }
    }
}
