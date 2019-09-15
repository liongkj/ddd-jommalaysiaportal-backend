using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetAllWorkflowUseCase : IGetAllWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;

        public GetAllWorkflowUseCase(IWorkflowRepository workflowRepository)
        {
            _workfowRepository = workflowRepository;
        }
        public async Task<bool> Handle(GetAllWorkflowRequest message, IOutputPort<GetAllWorkflowResponse> outputPort)
        {
            try
            {
                var response = await _workfowRepository.GetAllWorkflowByStatusAsync(message.Status);
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
