using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Update
{
    public class UpdatePublishedListing : IUpdatePublishListingUseCase
    {
        public Task<bool> Handle(CoreListingRequest message, IOutputPort<CreateWorkflowResponse> outputPort)
        {
            throw new System.NotImplementedException();
            //TODO
        }
    }
}