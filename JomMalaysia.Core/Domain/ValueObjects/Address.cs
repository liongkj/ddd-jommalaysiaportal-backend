using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Address : ValueObject
    {
        public Address(string add1, string add2, string city, string region, string postalCode, string country)
        {
            this.Add1 = add1;
            this.Add2 = add2;
            this.City = city;
            this.Region = region;
            this.PostalCode = postalCode;
            this.Country = country;

        }
        public string Add1 { get; private set; }
        public string Add2 { get; private set; }
        public string City { get; private set; }
        public string Region { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        private Address()
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
