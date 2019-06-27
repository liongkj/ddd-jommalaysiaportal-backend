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

        }
        public string MerchantId { get; private set; }
        public string CompanyName { get; private set; }
        public string CompanyRegistrationNumber { get; private set; }
        public Address Address { get; private set; }

        private readonly Collection<Listing> _listingItems;
        public IReadOnlyCollection<Listing> Listings => _listingItems;

        private readonly Collection<Contact> _contactItems;
        public IReadOnlyCollection<Contact> Contacts => _contactItems;

        public Merchant(string CompanyName, string CompanyRegistrationNumber, Address Address)
        {
            _listingItems = new Collection<Listing>();
            _contactItems = new Collection<Contact>();


            this.CompanyName = CompanyName;
            this.CompanyRegistrationNumber = CompanyRegistrationNumber;
            this.Address = Address ?? throw new Exception("Address is required");

        }

        public Listing AddListing(Listing newListing)
        {
            newListing.Merchant = this;
            _listingItems.Add(newListing);
            return newListing;
        }

        public bool RemoveListing(Listing removeListing)
        {
            if (removeListing.isPublish.IsPublished)
            {
                return false;
            }
            _listingItems.Remove(removeListing);
            return true;
        }

        public void AddContact(Contact c)
        {

            if (c != null)
            {
                _contactItems.Add(c);
            }
        }


        public void DeleteContact(string name, string email, string phone)
        {
            Contact UpdateContact = Contact.For(name, email, phone);

            if (_contactItems.Contains(UpdateContact))
            {
                _contactItems.Remove(UpdateContact);
            }
        }
    }
}
