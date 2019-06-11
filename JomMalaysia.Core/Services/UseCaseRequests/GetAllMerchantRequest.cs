using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class GetAllMerchantRequest:IUseCaseRequest<GetAllMerchantResponse>
    {
        public string CompanyName { get; }
        public string CompanyRegistrationNumber { get;}
        public string ContactFirstName { get; }
        public string ContactLastName { get; }
        public Address Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }

        public GetAllMerchantRequest()
        {
           
        }
    }
}
