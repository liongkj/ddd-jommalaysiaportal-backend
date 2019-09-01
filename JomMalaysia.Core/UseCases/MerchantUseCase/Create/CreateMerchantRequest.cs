using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Create
{
    public class CreateMerchantRequest : IUseCaseRequest<CreateMerchantResponse>
    {
        public CreateMerchantRequest(string CompanyName, CompanyRegistrationNumber CompanyRegistrationNumber, List<Contact> Contacts, Address Address)
        {
            this.CompanyName = CompanyName;
            this.CompanyRegistrationNumber = CompanyRegistrationNumber;
            this.Contacts = Contacts;
            this.Address = Address;
            Listings = new Collection<Listing>();
        }

        public string CompanyName { get; set; }
        public CompanyRegistrationNumber CompanyRegistrationNumber { get; set; }
        public Address Address { get; set; }
        public List<Contact> Contacts { get; set; }
        public ICollection<Listing> Listings { get; }

    }
}
