using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Publish
{
    public class UnpublishListingRequest : IUseCaseRequest<NewWorkflowResponse>
    {
        public string ListingId { get; set; }


    }
}
