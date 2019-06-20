using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.Listings.UseCaseResponses
{
    public class UpdateListingResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public UpdateListingResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UpdateListingResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}