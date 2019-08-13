using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;
using Xunit;

namespace JomMalaysia.Test.Core.ValueObject
{
    
    public class AddressTest
    {
        [Fact]
        public void NullTest()
        {
            Address address = new Address(" ","","","","","");
            Exception ex = Assert.Throws<ArgumentException>(() => address.Add1);
            Exception ex1 = Assert.Throws<ArgumentException>(() => address.Add2);
            Exception ex2 = Assert.Throws<ArgumentException>(() => address.City);
            Exception ex3 = Assert.Throws<ArgumentException>(() => address.State);
            Exception ex4 = Assert.Throws<ArgumentException>(() => address.PostalCode);
            Exception ex5 = Assert.Throws<ArgumentException>(() => address.Country);
            
        }

        [Fact]
        public void PostalCodeNumberTest()
        {
            Address adress = new Address("123", "456", "Segamat", "Johor", "Malaysia", "Malaysia");
            Exception ex = Assert.Throws<ArgumentException>(() => adress.PostalCode);
        }

        [Fact]
        public void ReturnTest()
        {
            Address adress = new Address("123", "456", "Segamat", "Johor", "85000", "Malaysia");
        }

    }
}
