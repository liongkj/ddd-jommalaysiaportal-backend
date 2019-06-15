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
        public UpdateMerchantRequest(string merchantId, string companyName, string companyRegistrationNumber, Name contactName, Address address, Phone phone, string fax, Email contactEmail)
        {
            if (string.IsNullOrWhiteSpace(MerchantId))
            {
                throw new System.ArgumentException("Select a Merchant", nameof(MerchantId));
            }
            Listings = new Collection<Listing>();
            this.CompanyName = companyName;
            this.CompanyRegistrationNumber = companyRegistrationNumber;
            this.ContactName = contactName;
            this.Address = address;
            this.Phone = phone;
            this.Fax = fax;
            this.ContactEmail = contactEmail;

        }
        public string MerchantId { get; set; }
        public string CompanyName { get; }
        public string CompanyRegistrationNumber { get; }
        public Name ContactName { get; }
        public Address Address { get; }
        public Phone Phone { get; }
        public string Fax { get; }
        public Email ContactEmail { get; }

        public ICollection<Listing> Listings { get; private set; }

    }
}