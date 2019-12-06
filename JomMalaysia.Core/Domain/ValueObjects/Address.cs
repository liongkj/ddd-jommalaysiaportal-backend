using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Validation;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Address : ValueObjectBase
    {
        public string Add1 { get; set; }
        public string Add2 { get; set; }
        public string City { get; set; }
        public StateEnum State { get; set; }
        public string PostalCode { get; set; }
        public CountryEnum Country { get; set; }
        public Location Location { get; private set; }

        private Address() { }
        public static Address For(string add1, string add2, string city, StateEnum state, string postalCode, CountryEnum country = CountryEnum.MY)
        {
            var add = new Address(add1, add2, city, state, postalCode, country);

            return add;
        }


        public Address(string add1, string add2, string city, StateEnum state, string postalCode, CountryEnum country)
        {
            this.Add1 = add1;
            this.Add2 = add2;
            this.City = city;
            this.State = state;
            this.PostalCode = postalCode;
            this.Country = country;
        }
        public Address(string add1, string add2, string city, StateEnum state, string postalCode, CountryEnum country, Coordinates coordinates)
        {
            this.Add1 = add1;
            this.Add2 = add2;
            this.City = city;
            this.State = state;
            this.PostalCode = postalCode;
            this.Country = country;
            Location = new Location(coordinates);
        }


        // private Address()
        // {

        // }

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
