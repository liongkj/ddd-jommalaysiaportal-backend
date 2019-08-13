using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class Contact : ValueObjectBase
    {

        private Contact()
        {

        }

        public Name Name { get; private set; }
        public Email Email { get; private set; }
        public Phone Phone { get; private set; }
        public bool IsPrimary { get; private set; }

        public Contact(string name, string email = null, string phone = null, bool IsPrimary = false)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ValidationException($"Contact {nameof(name)} should not be blank");
            }
            else
            {
                Name = (Name)name;
            }

            if (!string.IsNullOrEmpty(email))
            {
                Email = (Email)email;
            }

            if (!string.IsNullOrEmpty(phone))
            {
                Phone = (Phone)phone;
            }
            this.IsPrimary = IsPrimary;
        }

        public static Contact For(Name name, Email email, Phone phone)
        {
            var contact = new Contact
            {
                Name = name,
                Email = email,
                Phone = phone
            };
            return contact;
        }

        public Contact SetAsPrimary()
        {
            Contact Primary = new Contact(Name.ToString(), Email.ToString(), Phone.ToString(), true);
            return Primary;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }


    }
}
