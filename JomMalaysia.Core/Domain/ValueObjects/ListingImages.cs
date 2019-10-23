using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class ListingImages : ValueObjectBase
    {
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public List<string> ListingDetails { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
