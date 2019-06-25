using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Delete
{
    public class DeleteMerchantResponse : UseCaseResponseMessage
    {
        public DeleteMerchantResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public DeleteMerchantResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

    }
}