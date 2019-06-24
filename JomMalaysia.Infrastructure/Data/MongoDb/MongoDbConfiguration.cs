using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb
{

    public class MongoDbContext : IMongoDbConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public IMongoDatabase Database { get; }

        public IClientSessionHandle Session { get; private set; }

        private MongoClient _client;

        public MongoDbContext()
        {
            _client = new MongoClient(ConnectionString);
            Database = _client.GetDatabase(DatabaseName);
        }

        public async Task<IClientSessionHandle> StartSession(CancellationToken cancellactionToken = default)
        {
            var session = await _client.StartSessionAsync(cancellationToken: cancellactionToken);
            Session = session;
            return session;
        }
    }
}
