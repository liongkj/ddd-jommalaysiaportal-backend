using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Api.UseCases.Merchants
{
    public class MerchantViewModel
    {
        public string MerchantId { get; set; }
        public Name Name { get; set; }
        public string Email { get; set; }
        public string CompanyName { get; set; }
        public string CompanyRegistrationNumber { get; set; }
        public Address Address { get; set; }
        public Phone Phone { get; set; }
        public string Fax { get; set; }
    }
}
