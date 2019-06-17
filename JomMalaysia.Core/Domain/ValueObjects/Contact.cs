using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Contact:ValueObject
    {

        private Contact()
        {

        }


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
        
        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
    }
}
