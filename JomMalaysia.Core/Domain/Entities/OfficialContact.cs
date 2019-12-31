using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class OfficialContact: ValueObjectBase
    {
        public OfficialContact()
        {
            
        }
        public Phone MobileNumber { get; private set; }
        public Phone OfficeNumber { get; private set; }
        public string Website { get; private set; }
        public Phone Fax { get; private set; }
        public string Email { get; private set; }
        
        public OfficialContact(string mobile, string email = null, string website = null,string fax = null,string office = null)
        {
            MobileNumber = (Phone)mobile;
            OfficeNumber = (Phone)office;
            Website = website;
            Fax = (Phone)fax;
            Email = email;

        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}