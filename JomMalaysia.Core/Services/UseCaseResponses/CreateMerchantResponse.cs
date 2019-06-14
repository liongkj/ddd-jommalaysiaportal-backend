using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.UseCaseResponses
{
    public class CreateMerchantResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public CreateMerchantResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public CreateMerchantResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}
