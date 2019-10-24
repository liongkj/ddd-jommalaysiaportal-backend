using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Domain.Entities.Listings;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Get
{
    public class GetAllListingResponse : UseCaseResponseMessage
    {
        public List<Listing> Data { get; }
        public IEnumerable<string> Errors { get; }

        public GetAllListingResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetAllListingResponse(List<Listing> listings, bool success = false, string message = null) : base(success, message)
        {
            Data = listings;
        }
    }
}
