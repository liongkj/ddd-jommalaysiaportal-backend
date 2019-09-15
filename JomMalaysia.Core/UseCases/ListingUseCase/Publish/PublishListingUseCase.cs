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
        public async Task<bool> Handle(PublishListingRequest message, IOutputPort<PublishListingResponse> outputPort)
        {
            var requester = new User
            {
                Role = "admin"
            };
            //_loginInfo.AuthenticatedUser();


            //TODO find user by id/token
            //TODO check user level and assign workflow level

            //validate listingsid are real
            var getListingResponse = await _listingRepository.FindById(message.ListingId);

            //TODO check is there any request related to the listing

            if (getListingResponse.Success) //found listing
            {
                var ToBePublishListing = getListingResponse.Listing;
                //create new workflow objects
                Workflow PublishListingWorkflow = requester.PublishListing(ToBePublishListing);
                if (PublishListingWorkflow == null)//if not published
                {
                    outputPort.Handle(new PublishListingResponse(new List<string> { $"{ToBePublishListing.ListingName } is already published" }));
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
                        if (!createWorkflowResponse.Success)
                        {
                            await session.AbortTransactionAsync();
                            outputPort.Handle(new PublishListingResponse(createWorkflowResponse.Errors, false, createWorkflowResponse.Message));
                            return false;
                        }
                        // update listing status set to pending
                        var updateListingStatusResponse = await _listingRepository.UpdateAsyncWithSession(ToBePublishListing, session);

                        if (!updateListingStatusResponse.Success)
                        {
                            await session.AbortTransactionAsync();
                            outputPort.Handle(new PublishListingResponse(updateListingStatusResponse.Errors, false, updateListingStatusResponse.Message));
                            return false;
                        }

                        if (updateListingStatusResponse.Success && createWorkflowResponse.Success)
                        {
                            await session.CommitTransactionAsync();
                            outputPort.Handle(new PublishListingResponse(ToBePublishListing.ListingId + " workflow created successfully", true));
                            return true;
                        }
                        await session.AbortTransactionAsync();

                        outputPort.Handle(new PublishListingResponse(ToBePublishListing.ListingId + " error creating workflow"));
                        return false;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }

                }


            }
            outputPort.Handle(new PublishListingResponse(getListingResponse.Errors, false, getListingResponse.Message));
            return false;
        }

    }
}
