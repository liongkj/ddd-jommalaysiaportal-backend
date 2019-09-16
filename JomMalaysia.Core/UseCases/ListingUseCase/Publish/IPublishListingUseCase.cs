

using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Publish
{
    public interface IPublishListingUseCase : IUseCaseHandlerAsync<ListingWorkflowRequest, PublishListingResponse>
    {

    }
}
