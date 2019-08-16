using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Interfaces
{
    public interface IEntityDateTime
    {
        BsonDateTime CreatedAt { get; set; }
        BsonDateTime ModifiedAt { get; set; }
    }
}
