using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;

namespace JomMalaysia.Core.UseCases.ListingUseCase.Create
{
    public class PublishListingUseCase : IPublishListingUseCase
    {
        private readonly IListingRepository _listingRepository;
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IMongoDbContext _transaction;

        public PublishListingUseCase(IListingRepository listingRepository,
            IWorkflowRepository workflowRepository,
            IMongoDbContext transaction
        )
        {
            _listingRepository = listingRepository;
            _workflowRepository = workflowRepository;
            _transaction = transaction;
        }
        public bool Handle(PublishListingRequest message, IOutputPort<PublishListingResponse> outputPort)
        {
            var requester = new User(message.UserId);
            //find user by id/token
            //check user level and assign workflow level

            //validate listingsid are real
            var ListingIds = message.ListingIds;

            //check is there any request related to the listing


            //create new workflow objects
            if (message != null)
            {
                List<Workflow> NewWorkflows = new List<Workflow>();

                foreach (var id in ListingIds)
                {
                    List<string> ListingErrors = new List<string>();
                    //get listing
                    Listing PublishRequest = _listingRepository.FindById(id).Listing;
                    Workflow workflow = requester.PublishListing(PublishRequest);
                    //filter published listing
                    if (workflow != null)
                        NewWorkflows.Add(workflow);
                    else
                    {
                        ListingErrors.Add($"Listing {PublishRequest.ListingName} is already published");
                    }

                    //filter workflow with same type and same listing

                    //save into workflow database
                    _transaction.StartSession();
                    _transaction.Session.StartTransaction();
                    var result = _workflowRepository.CreateWorkflow(NewWorkflows);
                    //listing status set to pending

                    _transaction.Session.CommitTransaction();
                    if(result.Success)
                    {
                        if (ListingErrors.Count > 0)
                            outputPort.Handle(new PublishListingResponse(ListingErrors, true, "failed to publish"));
                        outputPort.Handle(new PublishListingResponse(result.Count, true, "are sent to publish"));
                    }
                    else //transaction failed
                    {
                        outputPort.Handle(new PublishListingResponse(result.Message, result.Success));
                    }
                    return result.Success;
                }
            }
            outputPort.Handle(new PublishListingResponse($"{nameof(message)} cannot be null", false));
            return false;
        }

    }
}
