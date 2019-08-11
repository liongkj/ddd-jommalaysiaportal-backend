using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;


namespace JomMalaysia.Core.Domain.Entities
{
    
    public abstract class Listing
    {
        //TODO : Factory Patter? Create Event, Government, Social and Private
        private string MerchantId { get; set; }
        public string ListingId { get; set; }
        public Merchant Merchant { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public ICollection<string> Tags { get; private set; }
        public Location ListingLocation { get; set; }
        public ListingImages ListingImages { get; set; }
        public string Status { get; set; }

        public Contact Contact { get; set; }
        public PublishStatus isPublish { get; set; }
        public ListingTypeEnum ListingType { get; set; }
        public CategoryPath Category { get; set; }

        public Listing()
        {

        }
        public Listing(string listingName, CategoryPath category,  Location listingLocation, ListingTypeEnum listingType)
        {
            ListingName = listingName;
            //Description = description;
            Category = category;
            //Subcategory = subcategory;
            ListingLocation = listingLocation;
            Tags = new Collection<string>();
            ListingType = listingType;

        }



        //public bool SetCategory(Category category, Subcategory Subcategory)
        //{
        //    this.Category = category;
        //    if (category.Subcategories.Contains(Subcategory))
        //    {
        //        this.Subcategory = Subcategory;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Please select a valid subcategory");
        //    }
        //    return true;
        //}
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

        //
        public void AddTags(string newTag)
        {
            
            if (Tags.Contains(newTag)) { Tags.Add(newTag); }
            else { throw new ArgumentException("Tag/keyword has already existed"); }
        }

        public void UpdateListing(Listing updated_listing)
        {
            string updateListingName(string new_name)
            {
                return ListingName = new_name;
            }

            string updateDescription(string new_description)
            {
                return Description = new_description;
            }

            CategoryPath updateCategory(CategoryPath new_category)
            {
                return Category = new_category;
            }

            Location updateLocation(Location new_location)
            {
                return ListingLocation = new_location;
            }

            ListingTypeEnum updateListingType(ListingTypeEnum new_ListingType)
            {
                return ListingType = new_ListingType;
            }

            //TODO: update collection?

            updateListingName(updated_listing.ListingName);
            updateDescription(updated_listing.Description);
            updateCategory(updated_listing.Category);
            updateLocation(updated_listing.ListingLocation);
            updateListingType(updated_listing.ListingType);
            
        }

        



    }
}
