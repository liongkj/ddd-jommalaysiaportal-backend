using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public void ChangeAddress()
        {

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Add1;
            yield return Add2;
            yield return City;
            yield return Region;
            yield return PostalCode;
            yield return Country;
        }
    }
}
