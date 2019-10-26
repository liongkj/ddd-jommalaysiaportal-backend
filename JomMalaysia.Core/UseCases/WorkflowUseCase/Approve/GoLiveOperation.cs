using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Approve
{
    public static class GoLiveOperation
    {
        public async static Task<bool> GoLive(IMongoDbContext _transaction, IOutputPort<WorkflowActionResponse> outputPort, Interfaces.Repositories.IWorkflowRepository _workflowRepository, IListingRepository _listingRepository, Workflow approvedWorkflow)
        {

            var findListingResponse = await _listingRepository.FindById(approvedWorkflow.Listing.ListingId);
            if (!findListingResponse.Success)
            {
                outputPort.Handle(new WorkflowActionResponse(findListingResponse.Errors, false, findListingResponse.Message));
                return false;
            }

            var GoLiveListing = findListingResponse.Listing;
            GoLiveListing.GoLive();

            using (var session = await _transaction.StartSession())
            {
                try
                {

                    session.StartTransaction();
                    //save workflow info
                    var updateWorkflowCommand = await _workflowRepository.UpdateAsync(approvedWorkflow, session);






                    //listing go live

                }
                catch (Exception e)
                {
                    outputPort.Handle(new WorkflowActionResponse(
                        "Publish listing operation failed",
                        false,
                         e.ToString()));
                    return false;
                }

            }
        }
    }
}