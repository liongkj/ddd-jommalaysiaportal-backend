using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.Merchants.UseCaseResponses
{
    public class UpdateMerchantResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public UpdateMerchantResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UpdateMerchantResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}