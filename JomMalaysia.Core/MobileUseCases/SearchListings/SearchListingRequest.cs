using System;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.MobileUseCases.SearchListings
{
    public class SearchListingRequest : IUseCaseRequest<ListingResponse>
    {
        public string q { get; set; }
        public string lang { get; set; }
    }
}