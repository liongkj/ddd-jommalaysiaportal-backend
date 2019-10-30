using System;
using System.ComponentModel.DataAnnotations;
using JomMalaysia.Core.Domain.ValueObjects;
using Xunit;

namespace Core.Test.ValueObject
{
    public class EmailTest
    {
        [Fact]
        public void EmailIsValid_IsTrue()
        {
            var emailString = "khaijiet@hotmail.com";
            var email = Email.For(emailString);
            Assert.Equal(email.Domain, "hotmail.com");
            Assert.Equal(email.User, "khaijiet");
        }


        [Fact]
        public void EmailIsInvalid_IsFalse()
        {
            var emailString = "khaijiethotmail.com";
            Action act = () => Email.For(emailString);
            Assert.Throws<Exception>(act);
        }
    }
}