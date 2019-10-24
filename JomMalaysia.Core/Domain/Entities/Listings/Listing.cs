using System;
using System.Collections.Generic;
using System.Linq;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    public abstract class Listing
    {
        public string ListingId { get; set; }
        public Merchant Merchant { get; set; }
        public string ListingName { get; set; }
        public string Description { get; set; }
        public ICollection<string> Tags { get; private set; }
        public Address Address { get; set; }
        public ListingImages ListingImages { get; set; }
        public ListingStatusEnum Status { get; set; }

        public Contact Contact { get; set; }
        public PublishStatus PublishStatus { get; set; }
        public ListingTypeEnum ListingType { get; set; }
        public CategoryPath Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public Listing()
        {

        }
        public Listing(string listingName, Merchant merchant, CategoryPath category, ListingTypeEnum listingType, ListingImages images, List<string> tags, string description, Address add)
        {
            Merchant = merchant;
            ListingName = listingName;
            Description = description;
            Category = category;
            ListingImages = images;
            Address = add;
            Tags = ValidateTags(tags);
            ListingType = listingType;
            Status = ListingStatusEnum.Pending;

        }

        public bool IsSafeToDelete()
        {
            return PublishStatus == null || !PublishStatus.IsPublished;
        }

        public bool IsEligibleToPublish()
        {
            return PublishStatus == null || !PublishStatus.IsPublished;
        }
        public bool IsEligibleToUnpublish()
        {
            return PublishStatus == null || PublishStatus.IsPublished;
        }

        public bool HasPrimaryContact()
        {
            return Contact != null;
        }
        public void UpdatePhoto() { }
        public void UpdateContact(Contact contact)
        {
            Contact = Contact.For(contact.Name, contact.Email, contact.Phone);
        }
        public void RemovePhoto() { }
        public void UpdateDescription() { }

        public void UpdateAds()
        {

        }

        public List<Merchant> SwitchOwnershipFromTo(Merchant Old, Merchant New)
        {
            List<Merchant> ToBeUpdateMerchants = new List<Merchant>();
            Old.RemoveListing(this);
            ToBeUpdateMerchants.Add(Old);
            New.AddNewListing(this);
            ToBeUpdateMerchants.Add(New);
            return ToBeUpdateMerchants;

        }
        public List<string> ValidateTags(List<string> tags)
        {
            var Clean = tags.Distinct().Where(s => s != null).ToList();
            List<string> CleanTags = new List<string>();

            foreach (var t in Clean)
            {
                var tag = t.Trim();
                tag = tag.ToLower();
                CleanTags.Add(tag);
            }
            return CleanTags;
        }

        internal void Updated()
        {
            ModifiedAt = DateTime.Now;
        }
    }
}
