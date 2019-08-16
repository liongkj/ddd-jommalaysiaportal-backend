using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class ListingImages : ValueObjectBase
    {
        public Uri ListingLogo { get; set; }
        public Uri CoverPhoto { get; set; }
        public Uri ExteriorPhoto { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
