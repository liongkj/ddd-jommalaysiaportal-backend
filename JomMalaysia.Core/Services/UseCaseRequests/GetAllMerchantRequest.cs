using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Services.UseCaseRequests
{
    public class GetAllMerchantRequest:IUseCaseRequest<GetAllMerchantResponse>
    {
        public string CompanyName { get; }
        public string CompanyRegistrationNumber { get;}
        public Address Address { get; set; }
        public Phone Phone { get; set; }
        public string Fax { get; set; }
        public Email ContactEmail { get; set; }
        public Name ContactName { get; set; }
        public ICollection<Listing> Listings { get; private set; }

        public GetAllMerchantRequest()
        {
            Listings = new Collection<Listing>();
        }
    }
}
