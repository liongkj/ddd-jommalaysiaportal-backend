using System;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.MobileUseCases.QueryListings
{
    public class QueryListingRequest : IUseCaseRequest<ListingResponse>
    {
        public QueryListingRequest(string categoryId, string type, bool groupBySub, string publishStatus, string selectedCity)
        {
            CategoryId = categoryId;
            Type = type;
            GroupBySub = groupBySub;
            PublishStatus = publishStatus;
            SelectedCity = selectedCity;
        }

        public string CategoryId { get; set; }
        public bool GroupBySub { get; set; }
        public string Type { get; set; }
        public string SelectedCity { get; set; }
        public string PublishStatus { get; set; }

    }
}