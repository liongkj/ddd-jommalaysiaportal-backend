using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.UseCases.ListingUseCase;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.ListingUseCase.Unpublish;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using JomMalaysia.Core.UseCases.WorkflowUseCase;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Approve;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Reject;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Workflows
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowsController : ControllerBase
    {
        #region dependencies
        private readonly IMapper _mapper;

        private readonly WorkflowPresenter _workflowPresenter;

        private readonly IPublishListingUseCase _publishListingUseCase;
        private readonly IUpdatePublishListingUseCase _updatePublishListingUseCase;
        private readonly IUnpublishListingUseCase _unpublishListingUseCase;
        private readonly IGetAllWorkflowUseCase _getAllWorkflowUseCase;
        private readonly IGetWorkflowUseCase _getWorkflowUseCase;
        private readonly IApproveWorkflowUseCase _approveWorkflowUseCase;
        private readonly IRejectWorkflowUseCase _rejectWorkflowUseCase;

        public WorkflowsController(IMapper mapper,
            IPublishListingUseCase PublishListingUseCase,
            IGetAllWorkflowUseCase getAllWorkflowUseCase,
            IGetWorkflowUseCase getWorkflowUseCase,
            WorkflowPresenter workflowPresenter,
            IApproveWorkflowUseCase approveWorkflowUseCase,
            IRejectWorkflowUseCase rejectWorkflowUseCase,
            IUnpublishListingUseCase unpublishListingUseCase,
            IUpdatePublishListingUseCase updatePublishListingUseCase
            )
        {
            _mapper = mapper;
            _getWorkflowUseCase = getWorkflowUseCase;
            _publishListingUseCase = PublishListingUseCase;
            _getAllWorkflowUseCase = getAllWorkflowUseCase;
            _workflowPresenter = workflowPresenter;
            _approveWorkflowUseCase = approveWorkflowUseCase;
            _rejectWorkflowUseCase = rejectWorkflowUseCase;
            _unpublishListingUseCase = unpublishListingUseCase;
            _updatePublishListingUseCase = updatePublishListingUseCase;
        }
        #endregion
        //publish a listing a start a approval workflow
        //PUT api/listings/{id}/publish
        [Route("~/api/listings/{ListingId}/publish")]
        [Route("~/api/listings/{ListingId}/publish/{months:int}")]
        [HttpPost]
        public async Task<IActionResult> Publish([FromRoute] string ListingId, [FromRoute] int months = 12)
        {

            var req = new ListingWorkflowRequest(ListingId);
            req.Months = months;

            await _publishListingUseCase.Handle(req, _workflowPresenter).ConfigureAwait(false);
            return _workflowPresenter.ContentResult;
        }

        [Route("~/api/listings/{ListingId}/unpublish")]
        [HttpPost]
        public async Task<IActionResult> Unpublish([FromRoute] string ListingId)
        {

            var req = new ListingWorkflowRequest(ListingId);

            await _unpublishListingUseCase.Handle(req, _workflowPresenter).ConfigureAwait(false);
            return _workflowPresenter.ContentResult;
        }

        [Route("~/api/listings/{ListingId}/update")]
        [HttpPost]
        public async Task<IActionResult> Update([FromRoute] string ListingId, [FromBody] CoreListingRequest ListingObject)
        {

            ListingObject.ListingId = ListingId;

            await _updatePublishListingUseCase.Handle(ListingObject, _workflowPresenter).ConfigureAwait(false);
            return _workflowPresenter.ContentResult;
        }

        //GET api/workflows
        //GET api/workflows/status/{pending}
        [Route("")]
        [Route("status/{status}")]
        [Route("status")]
        [HttpGet]
        public async Task<IActionResult> GetAllWorkflowByStatus([FromRoute]string status = "pending")
        {
            var req = new GetAllWorkflowRequest(status.ToLower());
            await _getAllWorkflowUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }


        //GET api/workflows/{id}
        [HttpGet("{id:length(24)}")]
        public async Task<IActionResult> GetWorkflowById([FromRoute]string id)
        {
            var req = new GetWorkflowRequest(id);
            await _getWorkflowUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }


        //PUT api/workflows/{id}/approve
        [HttpPut("{WorkflowId}/approve")]
        public async Task<IActionResult> Approve([FromRoute]string WorkflowId, [FromBody]WorkflowActionRequest req)
        {
            req.WorkflowId = WorkflowId;
            req.Action = "approve";
            await _approveWorkflowUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }

        //PUT api/workflows/{id}/reject
        [HttpPut("{WorkflowId}/reject")]
        public async Task<IActionResult> Reject([FromRoute]string WorkflowId, [FromBody]WorkflowActionRequest req)
        {
            req.WorkflowId = WorkflowId;
            req.Action = "reject";
            await _rejectWorkflowUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }

    }
}