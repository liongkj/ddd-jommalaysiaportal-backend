using System;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Publish
    {
        public bool IsPublished { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }
    }
}