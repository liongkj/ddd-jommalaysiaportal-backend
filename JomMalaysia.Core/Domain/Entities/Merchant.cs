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
        public CompanyRegistrationNumber CompanyRegistrationNumber { get; private set; }
        public Address Address { get; private set; }

        private readonly Collection<Listing> _listingItems;


        private readonly Collection<Contact> _contactItems;


        public Merchant(string CompanyName, CompanyRegistrationNumber CompanyRegistrationNumber, Address Address)
        {
            _listingItems = new Collection<Listing>();
            _contactItems = new Collection<Contact>();

            this.CompanyName = CompanyName ?? throw new Exception("Company Name is required");
            this.CompanyRegistrationNumber = CompanyRegistrationNumber ?? throw new Exception("Company Registration Number is required");
            this.Address = Address ?? throw new Exception("Address is required");

        }
        public bool IsSafeToDelete()
        {
            if (_listingItems.Count > 0)
            {
                return false;
            }
            return true;
        }
        public bool AddNewListingIsSuccess(Listing newListing)
        {

            return false;
        }

        public Collection<Listing> RemoveListing(Listing removeListing)
        {
            if (removeListing.isPublish.IsPublished)
            {
                throw new ArgumentException("Listing is still published");
            }
            _listingItems.Remove(removeListing);
            return _listingItems;
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
