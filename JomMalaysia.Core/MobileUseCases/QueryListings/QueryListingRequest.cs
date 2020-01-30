using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.MobileUseCases.QueryListings
{
    public class QueryListingRequest : IUseCaseRequest<ListingResponse>
    {

        public string CategoryId { get; set; }
        public bool GroupBySub { get; set; }
        public string Type { get; set; } = "all";
        public string SelectedCity { get; set; } = "";
        public string PublishStatus { get; set; } = "published";
        public bool Featured { get; set; }

    }
}