using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Domain.Enums;

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

        public List<Listing> Listings;


        public List<Contact> Contacts;



        public Merchant(string CompanyName, CompanyRegistrationNumber CompanyRegistrationNumber, Address Address)
        {
            Listings = new List<Listing>();
            Contacts = new List<Contact>();

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
        public Listing AddNewListing(Listing newListing)
        {
            //get public contact from merchant list of contacts
            foreach (var c in Contacts)
            {
                if (c.IsPrimary)
                {
                    newListing.Contact = c;
                    break;
                }
            }
            newListing.Merchant = this;
            newListing.Status = ListingStatusEnum.New;
            Listings.Add(newListing);
            return newListing;

        }

        public List<Listing> RemoveListing(Listing removeListing)
        {
            if (removeListing.IsSafeToDelete())
            {
                throw new ArgumentException("Listing is still published");
            }
            Listings.Remove(removeListing);
            return Listings;
        }

        public void AddContact(Contact c, List<Listing> listings = null)
        {
            if (c != null) //nullcheck
            {
                if (Contacts.Count < 1)//if no contacts, set as primary
                {
                    var PrimaryContact = c.SetAsPrimary();
                    Contacts.Add(PrimaryContact);

                    if (listings != null) //if merchant has listings
                    {
                        foreach (var list in listings)
                        {
                            list.UpdateContact(PrimaryContact);
                        }
                    }
                }
                else //add to back of list
                {
                    Contacts.Add(c);
                }


            }
        }



        //public void SetPrimaryContact(List<Contact> contacts, Contact c)
        //{
        //    //generate primary contact
        //    //change other contact to not primary
        //    //update lising contact
        //}


        #region helper

        #endregion
    }
}
