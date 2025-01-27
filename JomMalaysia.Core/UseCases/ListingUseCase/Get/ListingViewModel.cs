using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class ListingViewModel
    {
        public string ListingId { get; set; }
        public MerchantVM Merchant { get; set; }
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
            public string CategoryId { get; set; }
            public Category Category { get; set; }
            public Category Subcategory { get; set; }
            
            /// <summary>
            /// for Algolia multilingual datatype
            /// </summary>
            /// <param name="locale"></param>
            /// <returns>List of string according to locale</returns>
            public List<string> getCategoryFor(string locale)
            {
                locale = locale.ToLower();
                var keyword = new List<string>();
                switch (locale)
                {
                    case "ms": keyword.Add(Category.CategoryNameMs);
                        keyword.Add(Subcategory.CategoryNameMs);
                        break;
                
                    case "zh": keyword.Add(Category.CategoryNameZh);
                        keyword.Add(Subcategory.CategoryNameZh);
                        break;
                
                    default: keyword.Add(Category.CategoryName);
                        keyword.Add(Subcategory.CategoryName);
                        break;
                }

                return keyword;
            }
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

        public class MerchantVM
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