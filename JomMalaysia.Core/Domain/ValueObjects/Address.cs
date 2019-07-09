using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Address : ValueObjectBase
    {
        public string Add1 { get; private set; }
        public string Add2 { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string PostalCode { get; private set; }
        public string Country { get; private set; }

        public static Address For(string add1, string add2, string city, string state, string postalCode, string country="malaysia")
        {
            if (string.IsNullOrWhiteSpace(add1))
            {
                throw new ArgumentException("message", nameof(add1));
            }

            if (string.IsNullOrEmpty(add2))
            {
                throw new ArgumentException("message", nameof(add2));
            }

            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentException("message", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(state))
            {
                throw new ArgumentException("message", nameof(state));
            }

            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentException("message", nameof(postalCode));
            }

            if (string.IsNullOrWhiteSpace(country))
            {
                throw new ArgumentException("message", nameof(country));
            }

            var add = new Address();

            try
            {
                //format address 

            }
            catch (Exception e) { }
            return add;
        }



        public Address(string add1, string add2, string city, string state, string postalCode, string country)
        {
            this.Add1 = add1;
            this.Add2 = add2;
            this.City = city;
            this.State = state;
            this.PostalCode = postalCode;
            this.Country = country;

        }
       

        private Address()
        {

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Add1;
            yield return Add2;
            yield return City;
            yield return State;
            yield return PostalCode;
            yield return Country;
        }
    }
}
