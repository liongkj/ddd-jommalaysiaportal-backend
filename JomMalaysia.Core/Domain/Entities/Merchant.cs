using System.Collections.ObjectModel;
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
        public string MerchantId { get; private set; }
        public string CompanyName { get; private set; }
        public string CompanyRegistrationNumber { get; private set; }
        public Address Address { get; private set; }
        public ICollection<Listing> Listings { get; private set; }
        public ICollection<Contact> Contacts { get; private set; }

        public Merchant(string CompanyName, string CompanyRegistrationNumber, Address Address )
        {
            Listings = new Collection<Listing>();
            Contacts = new Collection<Contact>();
            
            this.CompanyName = CompanyName;
            this.CompanyRegistrationNumber = CompanyRegistrationNumber;
            this.Address = Address ?? throw new Exception("Address is required");

        }

        public Merchant(string CompanyName, string CompanyRegistrationNumber, Address Address, ICollection<Contact> contacts) : this(CompanyName, CompanyRegistrationNumber, Address)
        {
            Contacts = contacts;
        }
    }
}
