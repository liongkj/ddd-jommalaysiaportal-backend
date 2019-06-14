using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb
{

    public class ApplicationDbContext : IApplicationDbContext
    {
        public string CollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
