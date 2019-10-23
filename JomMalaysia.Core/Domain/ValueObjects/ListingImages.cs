using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class ListingImages : ValueObjectBase
    {
        public Image ListingLogo { get; set; }
        public Image CoverPhoto { get; set; }
        public List<Image> ListingDetails { get; set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
