using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
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
        public List<SubcategoryDto> Subcategories { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }

        public CategoryDto(string categoryName, string categoryNameMs, string categoryNameZh)
        {
            Subcategories = new List<SubcategoryDto>();
            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
        }

    }
}
