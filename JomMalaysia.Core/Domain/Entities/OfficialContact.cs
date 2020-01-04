using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class OfficialContact : ValueObjectBase
    {
        public OfficialContact()
        {

        }
        public Phone MobileNumber { get; private set; }
        public Phone OfficeNumber { get; private set; }
        public string Website { get; private set; }
        public Phone Fax { get; private set; }
        public string Email { get; private set; }

        public OfficialContact(string mobile = null, string email = null, string website = null, string fax = null, string office = null)
        {
            MobileNumber = String.IsNullOrEmpty(mobile) ? null : (Phone)mobile;
            OfficeNumber = String.IsNullOrEmpty(office) ? null : (Phone)office;
            Website = String.IsNullOrEmpty(website) ? null : website;
            Fax = String.IsNullOrEmpty(fax) ? null : (Phone)fax;
            Email = String.IsNullOrEmpty(email) ? null : email;

        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}