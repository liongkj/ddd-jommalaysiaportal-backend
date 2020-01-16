using System;
using System.Threading;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities.Listings;
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
            try
            {
                _client = new MongoClient(settings.ConnectionString);
                Database = _client.GetDatabase(settings.DatabaseName);
                //TODO check whether correct to create index here?
                var keys = Builders<ListingDto>.IndexKeys.Combine(
                    Builders<ListingDto>.IndexKeys.Text(x=>x.ListingName),
                    Builders<ListingDto>.IndexKeys.Text(x=>x.Tags),
                    Builders<ListingDto>.IndexKeys.Text(x=>x.Category.Category),
                    Builders<ListingDto>.IndexKeys.Text(x=>x.Category.Subcategory),
                    Builders<ListingDto>.IndexKeys.Text(x=>x.Description)) ;
                var indexOptions = new CreateIndexOptions { Background = true};
                var model = new CreateIndexModel<ListingDto>(keys, indexOptions);
                // Database.GetCollection<ListingDto>("Listing").Indexes.DropAll();
                Database.GetCollection<ListingDto>("Listing").Indexes.CreateOne(model);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<IClientSessionHandle> StartSession(CancellationToken cancellactionToken = default)
        {
            var session = await _client.StartSessionAsync(cancellationToken: cancellactionToken);
            Session = session;
            return session;
        }
    }
}
