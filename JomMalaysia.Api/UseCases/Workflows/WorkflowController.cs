using System.Collections.Generic;
using AutoMapper;
using JomMalaysia.Api.UseCases.Listings;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Workflows
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        private readonly IMapper _mapper;

        private readonly WorkflowPresenter _workflowPresenter;

        private readonly IPublishListingUseCase _publishListingUseCase;
        private readonly IGetAllWorkflowUseCase _getAllWorkflowUseCase;

        public WorkflowController(IMapper mapper, IPublishListingUseCase PublishListingUseCase,
            IGetAllWorkflowUseCase getAllWorkflowUseCase,
            WorkflowPresenter workflowPresenter
            )
        {
            _mapper = mapper;
            _publishListingUseCase = PublishListingUseCase;
            _getAllWorkflowUseCase = getAllWorkflowUseCase;
            _workflowPresenter = workflowPresenter;
        }

        //publish a listing a start a approval workflow
        //PUT api/listings/{id}/publish
        [Route("~/api/listings/publish")]
        [HttpPost]
        public IActionResult Publish([FromBody] List<string> ListingIds)
        {
            
            var req = new PublishListingRequest(ListingIds);

            _publishListingUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }

        //GET api/workflow/{status}
        [Route("")]
        [Route("{status}")]
        [HttpGet]
        public IActionResult GetWorkflowByStatus(string status = "")
        {
            var req = new GetAllWorkflowRequest(status.ToLower());
            _getAllWorkflowUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }

        //GET api/workflows
        //GET api/workflows/pending
        //GET api/workflows/{id}
        //PUT api/workflows/{id}/approve
        //PUT api/workflows/{id}/reject


    }
}