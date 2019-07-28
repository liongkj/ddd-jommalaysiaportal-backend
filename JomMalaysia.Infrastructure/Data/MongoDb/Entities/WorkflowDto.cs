using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class WorkflowDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Type { get; set; }
        public int Lvl { get; set; }

        public string ListingId { get; set; }
        public User Requester { get; set; }
        public User Responder { get; set; }
        public string Status { get; set; }
        public string Details { get; set; }
        public DateTime Created { get; set; }
        [BsonIgnoreIfNull]
        public ICollection<WorkflowDto> PreviousWorkflows { get; private set; }
    }
}
