using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.SharedRequest;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Create
{
    public class CreateMerchantRequest : IUseCaseRequest<CreateMerchantResponse>
    {
        // public CreateMerchantRequest(string CompanyName, CompanyRegistrationNumber CompanyRegistrationNumber, List<Contact> Contacts, Address Address)
        // {
        //     this.CompanyName = CompanyName;
        //     this.CompanyRegistrationNumber = CompanyRegistrationNumber;
        //     this.Contacts = Contacts;
        //     this.Address = Address;

        // }

        public string SsmId { get; set; }
        public string CompanyRegistrationName { get; set; }
        public AddressRequest Address { get; set; }
        public List<ContactRequest> Contacts { get; set; }
        // public List<Listing> Listings { get; }

    }
}
