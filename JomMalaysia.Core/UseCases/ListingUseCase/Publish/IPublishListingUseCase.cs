using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Publish
{
    public interface IPublishListingUseCase : IUseCaseHandlerAsync<PublishListingRequest, NewWorkflowResponse>
    {

    }
}
