using System;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class PublishStatusDto
    {
        public string Status { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }

    }
}