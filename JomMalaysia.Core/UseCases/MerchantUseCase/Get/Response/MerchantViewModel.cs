using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response
{
    public class MerchantViewModel
    {
        public string Id { get; set; }
        public CompanyRegistration CompanyRegistration { get; set; }
        public AddressViewModel Address { get; set; }
        public List<ContactViewModel> Contacts { get; set; }
        public List<string> ListingIds { get; set; }

    }
}