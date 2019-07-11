using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Contact : ValueObjectBase
    {

        private Contact()
        {

        }

        public Name Name { get; set; }
        public Email Email { get; set; }
        public Phone Phone { get; set; }

        public Contact(string name, string email, string phone)
        {

            Name = (Name)name;
            Email = (Email)email;
            Phone = (Phone)phone;

        }

        public static Contact For(string name, string email, string phone)
        {
            var contact = new Contact
            {
                Name = (Name)name,
                Email = (Email)email,
                Phone = (Phone)phone
            };
            return contact;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }

        
    }
}
