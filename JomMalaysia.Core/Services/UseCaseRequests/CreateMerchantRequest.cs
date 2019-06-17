using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class CreateMerchantRequest : IUseCaseRequest<CreateMerchantResponse>
    {
        public CreateMerchantRequest(string CompanyName, string CompanyRegistrationNumber, ICollection<Contact> Contacts, Address Address)
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
        public ICollection<Contact> Contacts { get; }
        public ICollection<Listing> Listings { get; }

    }
}
