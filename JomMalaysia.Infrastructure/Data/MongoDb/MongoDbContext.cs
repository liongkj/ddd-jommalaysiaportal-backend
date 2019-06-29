using System.Threading;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb
{

    public class MongoDbContext : IMongoDbContext
    {

        public IMongoDatabase Database { get; }

        public IClientSessionHandle Session { get; private set; }

        private MongoClient _client;

        public MongoDbContext(IMongoSettings settings)
        {
            _client = new MongoClient(settings.ConnectionString);
            Database = _client.GetDatabase(settings.DatabaseName);
        }

        public async Task<IClientSessionHandle> StartSession(CancellationToken cancellactionToken = default)
        {
            var session = await _client.StartSessionAsync(cancellationToken: cancellactionToken);
            Session = session;
            return session;
        }
    }
}
