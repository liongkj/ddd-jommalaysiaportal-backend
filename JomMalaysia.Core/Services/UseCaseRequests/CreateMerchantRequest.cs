using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class CreateMerchantRequest:IUseCaseRequest<CreateMerchantResponse>
    {
        public string CompanyName { get; }
        public string CompanyRegistrationNumber { get;}
        public string ContactFirstName { get; }
        public string ContactLastName { get; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public CreateMerchantRequest(string companyName, string companyRegistrationNumber, string contactFirstName,
            string contactLastName, Address address, string phone, string fax)
        {
            CompanyName = companyName;
            CompanyRegistrationNumber = companyRegistrationNumber;
            ContactFirstName = contactFirstName;
            ContactLastName = contactLastName;
            Address = address;
            Phone = phone;
            Fax = fax;
        }
    }
}
