using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities
{
    public abstract class Listing
    {
        //TODO : Factory Patter? Create Event, Government, Social and Private
        private string MerchantId;
        public string ListingId { get; set; }
        public Merchant Merchant { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public ICollection<string> Tags { get; private set; }
        public Location ListingLocation { get; set; }
        public string ListingLogo { get; set; }
        public string CoverPhoto { get; set; }
        public string ExteriorPhoto { get; set; }


        public Contact Contact { get; set; }
        public PublishStatus isPublish { get; set; }
        public ListingTypeEnum ListingType { get; set; }
        public Category Category { get; set; }
        public Subcategory Subcategory { get; set; }


        public Listing(string listingName, string description, Category category, Subcategory subcategory, Location listingLocation, ListingTypeEnum listingType)
        {
            ListingName = listingName;
            Description = description;
            Category = category;
            Subcategory = subcategory;
            ListingLocation = listingLocation;
            Tags = new Collection<string>();
            ListingType = listingType;
        }

        public bool SetCategory(Category category, Subcategory Subcategory)
        {
            this.Category = category;
            if (category.Subcategories.Contains(Subcategory))
            {
                this.Subcategory = Subcategory;
            }
            else
            {
                throw new ArgumentException("Please select a valid subcategory");
            }
            return true;
        }
        public void UpdatePhoto() { }
        public void UpdateContact(Contact contact)
        {
            Contact = Contact.For(contact.Name.ToString(), contact.Email.ToString(), contact.Phone.ToString());
        }
        public void RemovePhoto() { }
        public void UpdateDescription() { }

        public void UpdateAds()
        {

        }

        public void UpdateKeywords(Collection<string> tags)
        {

        }

    }
}
