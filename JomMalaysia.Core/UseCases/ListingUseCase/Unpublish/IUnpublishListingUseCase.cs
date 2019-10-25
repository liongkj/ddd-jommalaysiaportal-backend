using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Unpublish
{
    public interface IUnpublishListingUseCase : IUseCaseHandlerAsync<UnpublishListingRequest, NewWorkflowResponse>
    {

    }
}
