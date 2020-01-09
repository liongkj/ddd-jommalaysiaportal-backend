using System;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.MobileUseCases.QueryListings
{
    public class QueryListingRequest : IUseCaseRequest<ListingResponse>
    {
        public QueryListingRequest(string categoryId, string type, bool groupBySub, string publishStatus, string selectedDistrict)
        {
            CategoryId = categoryId;
            Type = type;
            GroupBySub = groupBySub;
            PublishStatus = publishStatus;
            SelectedDistrict = selectedDistrict;
        }

        public string CategoryId { get; set; }
        public bool GroupBySub { get; set; }
        public string Type { get; set; }
        public string SelectedDistrict { get; set; }
        public string PublishStatus { get; set; }

    }
}