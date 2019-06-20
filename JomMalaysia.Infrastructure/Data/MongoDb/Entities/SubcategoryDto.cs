using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class SubcategoryDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string SubcategoryName { get; set; }
        public string SubcategoryNameMs { get; set; }
        public string SubcategoryNameZh { get; set; }
        public ICollection<string> ListingIds { get; }
        public SubcategoryDto()
        {
            ListingIds = new Collection<string>();
        }
    }
}
