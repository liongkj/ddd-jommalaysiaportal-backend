using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Publish
{
    public class PublishListingRequest : IUseCaseRequest<NewWorkflowResponse>
    {
        public string ListingId { get; set; }
        public int Months { get; set; } = 12;

    }
}
