using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class CategoryDto
    {
        [BsonId]
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string CategoryName { get; set; }
        public CategoryPath CategoryPath { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }

        public CategoryDto(string categoryName, string categoryNameMs, string categoryNameZh, CategoryPath CategoryPath)
        {
            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
            this.CategoryPath = CategoryPath;
        }

    }
}
