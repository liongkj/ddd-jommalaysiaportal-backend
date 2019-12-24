using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;

namespace JomMalaysia.Core.MobileUseCases
{
    public class ListingResponse : UseCaseResponseMessage
    {
        public List<Listing> Listings { get; }
        public List<ListingViewModel> Data { get; }
        public IEnumerable<string> Errors { get; }

        public ListingResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ListingResponse(List<Listing> listings, bool success = false, string message = null) : base(success, message)
        {
            Listings = listings;
        }

        public ListingResponse(List<ListingViewModel> data, bool success = false, string message = null) : base(success, message)
        {
            Data = data;
        }
    }
}