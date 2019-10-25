using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Create
{
    public static class WorkflowFactory
    {
        public static async Task<bool> Create(IMongoDbContext _transaction, IWorkflowRepository _workflowRepository, IListingRepository _listingRepository, Workflow NewListingWorkflow, Listing TobeUpdateListing, IOutputPort<NewWorkflowResponse> outputPort)
        {
            using (var session = await _transaction.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    var createWorkflowResponse = await _workflowRepository.CreateWorkflowAsyncWithSession(NewListingWorkflow, session);
                    var updateListingStatusResponse = await _listingRepository.UpdateAsyncWithSession(TobeUpdateListing, session);
                    if (!createWorkflowResponse.Success)
                    {
                        outputPort.Handle(new NewWorkflowResponse(createWorkflowResponse.Errors, false, createWorkflowResponse.Message));
                        return false;
                    }
                    // update listing status set to pending


                    if (!updateListingStatusResponse.Success)
                    {
                        outputPort.Handle(new NewWorkflowResponse(updateListingStatusResponse.Errors, false, updateListingStatusResponse.Message));
                        return false;
                    }
                }
                catch
                {
                    await session.AbortTransactionAsync();
                    outputPort.Handle(new NewWorkflowResponse(TobeUpdateListing.ListingId + " error creating workflow"));
                    return false;
                }
                await session.CommitTransactionAsync();
                outputPort.Handle(new NewWorkflowResponse(TobeUpdateListing.ListingId + " workflow created successfully", true));
                return true;
            }
        }
    }
}