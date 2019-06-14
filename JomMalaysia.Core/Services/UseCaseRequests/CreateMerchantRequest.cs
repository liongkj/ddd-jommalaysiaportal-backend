using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class CreateMerchantRequest : IUseCaseRequest<CreateMerchantResponse>
    {
        public CreateMerchantRequest(string companyName, string companyRegistrationNumber, Name contactName, Address address, Phone phone, string fax, Email contactEmail)
        {
            this.CompanyName = companyName;
            this.CompanyRegistrationNumber = companyRegistrationNumber;
            this.ContactName = contactName;
            this.Address = address;
            this.Phone = phone;
            this.Fax = fax;
            this.ContactEmail = contactEmail;

        }
        public string CompanyName { get; }
        public string CompanyRegistrationNumber { get; }
        public Name ContactName { get; }
        public Address Address { get; }
        public Phone Phone { get; }
        public string Fax { get; }
        public Email ContactEmail { get; }


    }
}
