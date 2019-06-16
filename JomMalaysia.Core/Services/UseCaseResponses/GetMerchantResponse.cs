using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.UseCaseResponses
{
    public class GetMerchantResponse : UseCaseResponseMessage
    {
        public Merchant Merchant { get; }
        public IEnumerable<string> Errors { get; }

        public GetMerchantResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetMerchantResponse(Merchant merchant, bool success = false, string message = null) : base(success, message)
        {
            Merchant = merchant;
        }
    }
}
