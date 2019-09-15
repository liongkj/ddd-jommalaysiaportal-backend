using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.UseCases.ListingUseCase.Publish;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Workflows
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkflowController : ControllerBase
    {
        #region dependencies
        private readonly IMapper _mapper;

        private readonly WorkflowPresenter _workflowPresenter;

        private readonly IPublishListingUseCase _publishListingUseCase;
        private readonly IGetAllWorkflowUseCase _getAllWorkflowUseCase;
        private readonly IGetWorkflowUseCase _getWorkflowUseCase;

        public WorkflowController(IMapper mapper,
            IPublishListingUseCase PublishListingUseCase,
            IGetAllWorkflowUseCase getAllWorkflowUseCase,
            IGetWorkflowUseCase getWorkflowUseCase,
            WorkflowPresenter workflowPresenter
            )
        {
            _mapper = mapper;
            _getWorkflowUseCase = getWorkflowUseCase;
            _publishListingUseCase = PublishListingUseCase;
            _getAllWorkflowUseCase = getAllWorkflowUseCase;
            _workflowPresenter = workflowPresenter;
        }
        #endregion
        //publish a listing a start a approval workflow
        //PUT api/listings/{id}/publish
        [Route("~/api/listings/{ListingId}/publish")]
        [HttpPost]
        public async Task<IActionResult> Publish([FromRoute] string ListingId)
        {

            var req = new PublishListingRequest(ListingId);

            await _publishListingUseCase.Handle(req, _workflowPresenter).ConfigureAwait(false);
            return _workflowPresenter.ContentResult;
        }

        //GET api/workflows
        //GET api/workflows/pending
        //GET api/workflows/{status}
        [Route("")]
        [Route("{status}")]
        [HttpGet]
        public async Task<IActionResult> GetWorkflowByStatus(string status = "")
        {
            var req = new GetAllWorkflowRequest(status.ToLower());
            await _getAllWorkflowUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }


        //GET api/workflows/{id}
        [Route("{id}")]
        [HttpGet]
        public IActionResult GetWorkflowDetails([FromRoute]string workflowId)
        {
            var req = new GetWorkflowRequest(workflowId);
            _getWorkflowUseCase.Handle(req, _workflowPresenter);
            return _workflowPresenter.ContentResult;
        }

        //PUT api/workflows/{id}/approve
        //PUT api/workflows/{id}/reject


    }
}