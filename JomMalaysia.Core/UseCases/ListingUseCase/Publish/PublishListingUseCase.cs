using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class PublishListingUseCase : IPublishListingUseCase
    {
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
        public async Task<bool> Handle(ListingWorkflowRequest message, IOutputPort<ListingWorkflowResponse> outputPort)
        {
            var requester = _loginInfo.AuthenticatedUser();

            //TODO check user level and assign workflow level

            //validate listingsid are real
            var getListingResponse = await _listingRepository.FindById(message.ListingId);

            //TODO check is there any request related to the listing

            if (!getListingResponse.Success) //found listing
            {
                outputPort.Handle(new ListingWorkflowResponse(getListingResponse.Errors, false, getListingResponse.Message));
                return false;
            }
            var ToBePublishListing = getListingResponse.Listing;
            //create new workflow objects
            Workflow PublishListingWorkflow = requester.PublishListing(ToBePublishListing);
            if (PublishListingWorkflow == null)//if not published
            {
                outputPort.Handle(new ListingWorkflowResponse(new List<string> { $"{ToBePublishListing.ListingName } is already published" }));
                return false;
            }

            //TODO filter workflow with same type and same listing

            //save into workflow database

            using (var session = await _transaction.StartSession())
            {
                try
                {
                    session.StartTransaction();
                    var createWorkflowResponse = await _workflowRepository.CreateWorkflowAsyncWithSession(PublishListingWorkflow, session);
                    var updateListingStatusResponse = await _listingRepository.UpdateAsyncWithSession(ToBePublishListing, session);
                    if (!createWorkflowResponse.Success)
                    {
                        outputPort.Handle(new ListingWorkflowResponse(createWorkflowResponse.Errors, false, createWorkflowResponse.Message));
                        return false;
                    }
                    // update listing status set to pending


                    if (!updateListingStatusResponse.Success)
                    {
                        outputPort.Handle(new ListingWorkflowResponse(updateListingStatusResponse.Errors, false, updateListingStatusResponse.Message));
                        return false;
                    }
                }
                catch
                {
                    await session.AbortTransactionAsync();
                    outputPort.Handle(new ListingWorkflowResponse(ToBePublishListing.ListingId + " error creating workflow"));
                    return false;
                }
                await session.CommitTransactionAsync();
                outputPort.Handle(new ListingWorkflowResponse(ToBePublishListing.ListingId + " workflow created successfully", true));
                return true;
            }
        }

    }


}
