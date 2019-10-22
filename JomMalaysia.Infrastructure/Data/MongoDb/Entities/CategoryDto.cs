﻿using System;
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
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        [BsonIgnoreIfNull]
        public string ParentCategory { get; set; }
        public string CategoryPath { get; set; }
        public string Url { get; set; }
        public string ThumbnailUrl { get; set; } = "https://res.cloudinary.com/jomn9-com/image/upload/v1571632729/category_thumbnail/proz1gzlepy9gp3rk0ej.jpg";


        public CategoryDto()
        {

        }



    }
}
