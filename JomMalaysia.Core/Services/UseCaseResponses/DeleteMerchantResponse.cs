using System.Collections.Generic;

namespace JomMalaysia.Core.Interfaces
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

        public DeleteMerchantResponse()
        {

        }
    }
}