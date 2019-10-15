using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.MobileUseCases
{
    public class ListingResponse : UseCaseResponseMessage
    {
        public List<Listing> Data { get; }
        public IEnumerable<string> Errors { get; }

        public ListingResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ListingResponse(List<Listing> data, bool success = false, string message = null) : base(success, message)
        {
            Data = data;
        }
    }
}