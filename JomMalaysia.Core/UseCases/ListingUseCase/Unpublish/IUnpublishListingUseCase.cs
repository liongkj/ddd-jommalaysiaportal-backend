

using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Unpublish
{
    public interface IUnpublishListingUseCase : IUseCaseHandlerAsync<ListingWorkflowRequest, ListingWorkflowResponse>
    {

    }
}
