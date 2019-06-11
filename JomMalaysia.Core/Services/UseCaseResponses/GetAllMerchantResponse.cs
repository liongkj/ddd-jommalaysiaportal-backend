using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.UseCaseResponses
{
    public class GetAllMerchantResponse : UseCaseResponseMessage
    {
        public List<Merchant> Merchants { get; }
        public IEnumerable<string> Errors { get; }

        public GetAllMerchantResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetAllMerchantResponse(List<Merchant> merchants, bool success = false, string message = null) : base(success, message)
        {
            Merchants = merchants;
        }
    }
}
