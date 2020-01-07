
using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Update
{
    public class UpdateMerchantRequest : IUseCaseRequest<UpdateMerchantResponse>
    {

        public string MerchantId { get; }
        public string SsmId { get; set; }
        public string OldSsmId { get; set; }
        public string CompanyRegistrationName { get; set; }
        public AddressRequest Address { get; set; }
        public List<ContactRequest> Contacts { get; set; } = new List<ContactRequest>();
        public List<string> Listings { get; }

    }
}