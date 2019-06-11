using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Merchant
    {
        public Merchant()
        {
            Listings = new HashSet<Listing>();
        }

        public string MerchantId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        
        public ICollection<Listing> Listings { get; private set; }
        public Merchant(string CompanyName, string CompanyRegistrationNumber, string ContactFirstName, string ContactLastName, Address Address, string Phone, string Fax)
        {
            this.CompanyName = CompanyName;
            this.CompanyRegistrationNumber = CompanyRegistrationNumber;
            this.ContactFirstName = ContactFirstName;
            this.ContactLastName = ContactLastName;
            this.Address = Address;
            this.Phone = Phone;
            this.Fax = Fax;
        }
    }
}

