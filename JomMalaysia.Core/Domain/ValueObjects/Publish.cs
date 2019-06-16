using System;
using System.Collections.Generic;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Publish : ValueObject
    {

        //TODO
        public bool IsPublished { get; set; }
        public DateTime ValidityStart { get; set; }
        public DateTime ValidityEnd { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}