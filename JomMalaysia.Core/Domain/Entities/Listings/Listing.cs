using System;
using System.Collections.Generic;
using System.Linq;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;

namespace JomMalaysia.Core.Domain.Entities.Listings
{
    public abstract class Listing
    {
        public string ListingId { get; set; }
        public Merchant Merchant { get; set; }
        public string ListingName { get; set; }
        public Description Description { get; set; }
        public ICollection<string> Tags { get; private set; }
        public Address Address { get; set; }
        public CategoryPath Category { get; set; }
        public CategoryType CategoryType { get; set; }
        public ListingImages ListingImages { get; set; }
        // public ListingStatusEnum Status { get; set; }
        public bool IsFeatured { get; set; } = false;

        public OfficialContact OfficialContact { get; set; }
        public List<StoreTimes> OperatingHours { get; set; }
        public PublishStatus PublishStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        protected Listing()
        {

        }

        protected Listing(string listingName, Merchant merchant, CategoryType categoryType, CategoryPath category, ListingImagesRequest images,
            ICollection<string> tags, Description description, Address add, List<StoreTimesRequest> operatingHours, OfficialContact officialContact, PublishStatus publishStatus)
        {
            Merchant = merchant;
            ListingName = listingName;
            Description = description;
            Category = category;
            ListingImages = GenerateImage(images);
            Address = add;
            Tags = tags;
            CategoryType = categoryType;
            PublishStatus = publishStatus ?? new PublishStatus();
            OperatingHours = PopulateOperatingHours(operatingHours);
            OfficialContact = officialContact;

        }


        public bool IsSafeToDelete()
        {
            return !IsPublished();
        }

        public bool IsEligibleToPublish()
        {
            return !IsPublished();
        }

        public bool IsPublished()
        {
            return Equals(PublishStatus.Status, ListingStatusEnum.Published);
        }

        public bool IsEligibleToUnpublish()
        {
            return IsPublished();
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

        private ListingImages GenerateImage(ListingImagesRequest images)
        {

            var ads = new List<Image>();
            if (images.Ads != null)
            {
                foreach (var ad in images.Ads)
                {
                    ads.Add(new Image(ad.Url));
                }
            }

            var listingImages = new ListingImages
            {
                ListingLogo = new Image(images.ListingLogo.Url),
                CoverPhoto = new Image(images.CoverPhoto.Url),
                Ads = ads
            };
            return listingImages;
        }

        private List<StoreTimes> PopulateOperatingHours(List<StoreTimesRequest> OperatingHours)
        {
            List<StoreTimes> store = new List<StoreTimes>();
            foreach (var t in OperatingHours)
            {
                store.Add(new StoreTimes(t.Day, t.OpenTime, t.CloseTime));
            }

            return store;
        }
        internal void Updated()
        {
            ModifiedAt = DateTime.Now;
        }

        internal void GoLive(Workflow workflow)
        {
            PublishStatus.Publish(workflow.Months);
        }
    }
}
