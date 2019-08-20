using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Validation;

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
            var add = new Address(add1,add2,city,state,postalCode,country);

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
