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

        public Collection<Listing> Listings;


        public Collection<Contact> Contacts;

        

        public Merchant(string CompanyName, CompanyRegistrationNumber CompanyRegistrationNumber, Address Address)
        {
            Listings = new Collection<Listing>();
            Contacts = new Collection<Contact>();

            this.CompanyName = CompanyName ?? throw new Exception("Company Name is required");
            this.CompanyRegistrationNumber = CompanyRegistrationNumber ?? throw new Exception("Company Registration Number is required");
            this.Address = Address ?? throw new Exception("Address is required");

        }
        public bool IsSafeToDelete()
        {
            if (Listings.Count > 0)
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
            Listings.Remove(removeListing);
            return Listings;
        }

        public void AddContact(Contact c)
        {

            if (c != null)
            {
                Contacts.Add(c);
            }
        }


        public void DeleteContact(string name, string email, string phone)
        {
            Contact UpdateContact = Contact.For(name, email, phone);

            if (Contacts.Contains(UpdateContact))
            {
                Contacts.Remove(UpdateContact);
            }
        }
    }
}
