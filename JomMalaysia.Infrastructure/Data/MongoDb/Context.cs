using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace JomMalaysia.Infrastructure.Data.MongoDb
{



    public class Context
    {

        private readonly MongoClient _mongoClient;
        private readonly IMongoDatabase _database;


        public Context(string connectionString, string databaseName)
        {
            _mongoClient = new MongoClient(connectionString);
            _database = _mongoClient.GetDatabase(databaseName);
            Map();
        }

        internal IMongoCollection<Listing> Listings
        {
            get
            {
                return _database.GetCollection<Listing>("Listings");
            }
        }
        internal IMongoCollection<Merchant> Merchants
        {
            get
            {
                return _database.GetCollection<Merchant>("Customers");
            }


        }
        private void Map()
        {
            BsonClassMap.RegisterClassMap<Merchant>(cm =>
            {
                cm.AutoMap();
            });

            BsonClassMap.RegisterClassMap<Listing>(cm =>
            {
                cm.AutoMap();
            });


        }
    }
}
