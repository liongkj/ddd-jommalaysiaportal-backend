using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class UpdateMerchantRequest : IUseCaseRequest<UpdateMerchantResponse>
    {
        public UpdateMerchantRequest(string companyName, string companyRegistrationNumber, Contact Contact, Address address)
        {
            this.CompanyName = companyName;
            this.CompanyRegistrationNumber = companyRegistrationNumber;
            this.Contact = Contact;
            this.Address = address;
        }
        public string MerchantId { get; }
        public string CompanyName { get; }
        public string CompanyRegistrationNumber { get; }
        public Address Address { get; }

        public Contact Contact { get; }


    }
}