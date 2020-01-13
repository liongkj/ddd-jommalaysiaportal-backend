using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.CategoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class ListingViewModel
    {
        public string ListingId { get; set; }
        public MerchantViewModel Merchant { get; set; }
        public string ListingName { get; set; }
        public Description Description { get; set; }
        public AddressViewModel Address { get; set; }
        public List<StoreTimesViewModel> OperatingHours { get; set; }

        public CategoryPathViewModel Category { get; set; }
        public string CategoryType { get; set; }

        public ICollection<string> Tags { get; set; }
        public ListingImages ListingImages { get; set; }
        public OfficialContactViewModel OfficialContact { get; set; }
        public bool IsFeatured { get; set; } = false;


        public PublishStatusViewModel PublishStatus { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }


        public class CategoryPathViewModel
        {
            public String CategoryId { get; set; }
            public Category Category { get; set; }
            public Category Subcategory { get; set; }
        }

        public class StoreTimesViewModel
        {
            public string DayOfWeek { get; set; }
            public TimeSpan OpenTime { get; set; }
            public TimeSpan CloseTime { get; set; }

        }

        public class OfficialContactViewModel
        {
            public string MobileNumber { get; set; }
            public string OfficeNumber { get; set; }
            public string Website { get; set; }
            public string Fax { get; set; }
            public string Email { get; set; }

        }


        public class AddressViewModel
        {
            public string Add1 { get; set; }
            public string Add2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string PostalCode { get; set; }
            public string Country { get; set; }
            public Coordinates Coordinates { get; set; }
        }

        public class MerchantViewModel
        {
            public string MerchantId { get; set; }
            public string SsmId { get; set; }
            public string RegistrationName { get; set; }
        }
        public class PublishStatusViewModel
        {
            public string Status { get; set; }
            public DateTime? ValidityStart { get; set; }
            public DateTime? ValidityEnd { get; set; }
        }
    }
}