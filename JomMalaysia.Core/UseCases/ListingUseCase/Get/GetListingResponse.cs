using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetListingResponse : UseCaseResponseMessage
    {
        public Listing Listing { get; }
        public IEnumerable<string> Errors { get; }

        public GetListingResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetListingResponse(Listing listing, bool success = false, string message = null) : base(success, message)
        {
            Listing = listing;
        }
    }
}
