using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.ValueObjects;
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
            var requester = _loginInfo.AuthenticatedUser();
            var Errors = new List<string>();

            //find user by id/token
            //check user level and assign workflow level

            //validate listingsid are real
            var ToBePublishListing = (await _listingRepository.FindById(message.ListingId)).Listing;
            
            //check is there any request related to the listing
            //if(PublishRequest.status workflow) //stop workflow
            if (ToBePublishListing != null)
            {
                
                    //create new workflow objects
                    Workflow PublishListingWorkflow = requester.PublishListing(ToBePublishListing);
                if (PublishListingWorkflow != null)//if not published
                {
                    PublishListingWorkflow.Start();
                }
                else
                {
                    Errors.Add($"Listing {ToBePublishListing.ListingName} is already published");
                }

                //filter workflow with same type and same listing

                //save into workflow database
                var result = new CreateWorkflowResponse(Errors);
                using (var session = await _transaction.StartSession())
                {
                    session.StartTransaction();
                    result = _workflowRepository.CreateWorkflow(PublishListingWorkflow, session);
                    //listing status set to pending

                    _transaction.Session.CommitTransaction();
                }
               
                if (result.Success) //transaction commited
                {
                    outputPort.Handle(new PublishListingResponse(result.Id, true,"Workflow Started Successfully"));
                }
                else //transaction failed
                {
                    outputPort.Handle(new PublishListingResponse(result.Errors));
                }
                return result.Success;
            }

            outputPort.Handle(new PublishListingResponse($"{nameof(message)} cannot be null"));
            return false;
        }

    }
}
