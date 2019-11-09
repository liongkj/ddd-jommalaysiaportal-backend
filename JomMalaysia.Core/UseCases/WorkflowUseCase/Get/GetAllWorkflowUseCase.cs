using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetAllWorkflowUseCase : IGetAllWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;
        private readonly IMapper _mapper;

        public GetAllWorkflowUseCase(IWorkflowRepository workflowRepository, IMapper mapper)
        {
            _workfowRepository = workflowRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetAllWorkflowRequest message, IOutputPort<GetAllWorkflowResponse> outputPort)
        {
            try
            {
                var queryAllWorkflow = await _workfowRepository.GetAllWorkflowByStatusAsync(message.Status);
                if (!queryAllWorkflow.Success)
                {
                    outputPort.Handle(queryAllWorkflow);
                    return false;
                }
                var mapped = _mapper.Map<List<WorkflowViewModel>>(queryAllWorkflow.Workflows);
                GetAllWorkflowResponse response = new GetAllWorkflowResponse(mapped, queryAllWorkflow.Success, queryAllWorkflow.Message);

                outputPort.Handle(response);
                return response.Success;
            }
            catch (Exception e)
            {
                outputPort.Handle(new GetAllWorkflowResponse(new List<string> { "Get Workflow Error" }, false, e.ToString()));
                return false;
            }

        }
    }
}
