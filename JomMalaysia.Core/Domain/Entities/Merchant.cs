using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Entities.Listings;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Merchant
    {
        public Merchant()
        {

        }
        public string MerchantId { get; private set; }
        public CompanyRegistration CompanyRegistration { get; private set; }
        public Address Address { get; private set; }

        public List<string> Listings { get; set; }


        public List<Contact> Contacts { get; set; }



        public Merchant(CompanyRegistration companyRegistration, Address address)
        {
            Listings = new List<string>();
            Contacts = new List<Contact>();

            CompanyRegistration = companyRegistration;
            Address = address;

        }
        public bool IsSafeToDelete()
        {
            if (Listings.Count > 0)
            {
                return false;
            }
            return true;
        }
        public void AddNewListing(Listing newListing)
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

            Listings.Add(newListing.ListingId);
        }

        public void RemoveListing(Listing removeListing)
        {

            Listings.Remove(removeListing.ListingId);

        }

        public void AddContact(Contact c, List<Listing> listings = null)
        {
            if (c != null) //nullcheck
            {
                if (Contacts.Count < 1)//if no contacts, set as primary
                {
                    var PrimaryContact = c.SetAsPrimary(c);
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
