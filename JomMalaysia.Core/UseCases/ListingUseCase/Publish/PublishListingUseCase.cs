using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Publish
{
    public class PublishListingUseCase : IPublishListingUseCase
    {
        #region dependencies
        private readonly IListingRepository _listingRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMongoDbContext _transaction;
        private readonly ILoginInfoProvider _loginInfo;
        public PublishListingUseCase(
            IListingRepository listingRepository,
            IWorkflowRepository workflowRepository,
            ILoginInfoProvider loginInfoProvider,
            IMongoDbContext transaction
        )
        {
            _listingRepository = listingRepository;
            _workflowRepository = workflowRepository;
            _transaction = transaction;
            _loginInfo = loginInfoProvider;
        }
        #endregion
        public async Task<bool> Handle(PublishListingRequest message, IOutputPort<NewWorkflowResponse> outputPort)
        {
            var requester = _loginInfo.AuthenticatedUser();


            //validate listingsid are real
            var getListingResponse = await _listingRepository.FindById(message.ListingId);
            if (!getListingResponse.Success) //found listing
            {
                outputPort.Handle(new NewWorkflowResponse(getListingResponse.Errors, false, getListingResponse.Message));
                return false;
            }
            var ToBePublishListing = getListingResponse.Listing;

            //check is there any request related to the listing
            bool ListingHasPendingWorkflows = await _workflowRepository.GetPendingWorkflowForListing(ToBePublishListing.ListingId);
            if (ListingHasPendingWorkflows)
            {
                outputPort.Handle(new NewWorkflowResponse(new List<string> { $"{ToBePublishListing.ListingName } has pending workflows, please complete the workflow before creating a new one." }));
                return false;
            }

            //create new workflow objects
            Workflow PublishListingWorkflow = requester.PublishListing(ToBePublishListing, message.Months);
            if (PublishListingWorkflow == null)//if not published
            {
                outputPort.Handle(new NewWorkflowResponse(new List<string> { $"{ToBePublishListing.ListingName } is already published" }));
                return false;
            }

            //save into workflow database
            return await WorkflowFactory.Create(_transaction, _workflowRepository, _listingRepository, PublishListingWorkflow, ToBePublishListing, outputPort);

        }

    }


}
