using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetWorkflowUseCase : IGetWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;
        private readonly IListingRepository _listingRepository;
        private readonly IMapper _mapper;

        public GetWorkflowUseCase(IWorkflowRepository workflowRepository, IListingRepository listingRepository, IMapper mapper)
        {
            _workfowRepository = workflowRepository;
            _listingRepository = listingRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetWorkflowRequest message, IOutputPort<GetWorkflowResponse> outputPort)
        {
            var workflowResponse = await _workfowRepository.GetWorkflowByIdAsync(message.WorkflowId);
            if (!workflowResponse.Success)
            {
                outputPort.Handle(workflowResponse);
                return false;
            }
            var mapped = _mapper.Map<WorkflowViewModel>(workflowResponse.Workflow);
            GetWorkflowResponse response = new GetWorkflowResponse(mapped, workflowResponse.Success, workflowResponse.Message);

            outputPort.Handle(response);
            return response.Success;
        }
    }
}
