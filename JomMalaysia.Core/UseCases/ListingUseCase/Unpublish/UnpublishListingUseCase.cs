﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Unpublish
{
    public class UnpublishListingUseCase : IUnpublishListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMongoDbContext _transaction;
        private readonly ILoginInfoProvider _loginInfo;
        public UnpublishListingUseCase(
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
        public async Task<bool> Handle(UnpublishListingRequest message, IOutputPort<NewWorkflowResponse> outputPort)
        {
            var requester = _loginInfo.AuthenticatedUser();

            //validate listingsid are real
            var getListingResponse = await _listingRepository.FindById(message.ListingId);

            if (!getListingResponse.Success) //found listing
            {
                outputPort.Handle(new NewWorkflowResponse(getListingResponse.Errors, false, getListingResponse.Message));
                return false;
            }

            var ToBeDeleted = getListingResponse.Listing;

            //check is there any request related to the listing
            bool ListingHasPendingWorkflows = await _workflowRepository.GetPendingWorkflowForListing(ToBeDeleted.ListingId);
            if (ListingHasPendingWorkflows)
            {
                outputPort.Handle(new NewWorkflowResponse(new List<string> { $"{ToBeDeleted.ListingName } has pending workflows, please complete the workflow before creating a new one." }));
                return false;
            }

            //create new workflow
            Workflow UnpublishListingWorkflow = requester.UnpublishListing(ToBeDeleted);

            if (UnpublishListingWorkflow == null)
            {
                outputPort.Handle(new NewWorkflowResponse(new List<string> { "Listing is not published" }));
                return false;
            }

            using (var session = await _transaction.StartSession())
            {
                session.StartTransaction();
                NewWorkflowResponse NewWorkflowResponse;
                try
                {
                    NewWorkflowResponse = await _workflowRepository.CreateWorkflowAsyncWithSession(UnpublishListingWorkflow, session);
                }
                catch
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
                await session.CommitTransactionAsync();
                outputPort.Handle(NewWorkflowResponse);
                return NewWorkflowResponse.Success;
            }
            //TODO background taks to unpublish validity end https://docs.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-3.0&tabs=visual-studio

        }
    }

}
