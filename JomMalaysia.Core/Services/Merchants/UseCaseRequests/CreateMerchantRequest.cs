using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;

namespace JomMalaysia.Core.Services.Merchants.UseCaseRequests
{
    public class CreateMerchantRequest : IUseCaseRequest<CreateMerchantResponse>
    {
        public CreateMerchantRequest(string CompanyName, string CompanyRegistrationNumber, IReadOnlyCollection<Contact> Contacts, Address Address)
        {
            this.CompanyName = CompanyName;
            this.CompanyRegistrationNumber = CompanyRegistrationNumber;
            this.Contacts = Contacts;
            this.Address = Address;
            Listings = new Collection<Listing>();
        }
        
        public string CompanyName { get; }
        public string CompanyRegistrationNumber { get; }
        public Address Address { get; }
        public IReadOnlyCollection<Contact> Contacts { get; }
        public ICollection<Listing> Listings { get; }

    }
}
