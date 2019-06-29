using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class CreateListingResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public CreateListingResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public CreateListingResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}
