using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Infrastructure.Data.MongoDb.Entities
{
    public class ImageDto
    {
        public string Id { get; set; }
        public byte[] BinaryData { get; set; }
    }
}
