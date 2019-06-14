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
        public Name ContactName { get; private set; }
        public Address Address { get; private set; }
        public Email ContactEmail { get; private set; }
        public Phone Phone { get; private set; }
        public string Fax { get; private set; }
        public ICollection<Listing> Listings { get; private set; }

        public Merchant(string CompanyName, string CompanyRegistrationNumber, Name ContactName, Address Address, Phone Phone, string Fax, Email Email)
        {
            Listings = new Collection<Listing>();
            this.ContactEmail = Email ?? throw new Exception("Email is required");
            this.CompanyName = CompanyName;
            this.CompanyRegistrationNumber = CompanyRegistrationNumber;
            this.ContactName = ContactName ?? throw new Exception("Name is required");
            this.Address = Address ?? throw new Exception("Address is required");
            this.Phone = Phone;
            this.Fax = Fax;
        }

        public void Delete(Merchant m)
        {

        }

        public void UpdatePhone(string no)
        {
            var OldPhone = Phone;
            var NewPhone = Phone.For(no);
            Phone = NewPhone;
            //update db
        }

        public void AddListing(Listing Listing)
        {
            Listings.Add(Listing);
        }

        public void DeleteListing(Listing ListingId)
        {
            foreach (Listing l in Listings)
            {
                if (l.ListingId.Equals(ListingId))
                    l.Delete();
                throw new Exception("Listing not found");
            }
        }


    }
}
