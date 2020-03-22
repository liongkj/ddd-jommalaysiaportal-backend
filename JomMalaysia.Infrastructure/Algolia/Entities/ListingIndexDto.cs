using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Infrastructure.Algolia.Entities
{
    public class ListingIndexDto
    {
        public string ObjectId { get; set; }
        public MerchantIndexDto Merchant { get; set; }
        public string ListingName { get; set; }
        public Description Description { get; set; }
        public AddressViewModel Address { get; set; }
        public List<ListingViewModel.StoreTimesViewModel> OperatingHours { get; set; }

        public ListingViewModel.CategoryPathViewModel Category { get; set; }
        public string CategoryType { get; set; }

        public ICollection<string> Tags { get; set; }
        
    }

    public class MerchantIndexDto
    {
        public string SsmId { get; set; }
        public string RegistrationName { get; set; }
    }
}