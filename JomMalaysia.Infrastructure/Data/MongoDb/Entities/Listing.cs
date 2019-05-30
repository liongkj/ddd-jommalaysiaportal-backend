using System;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    class Listing
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Merchant Merchant { get; set; }
    }
}
