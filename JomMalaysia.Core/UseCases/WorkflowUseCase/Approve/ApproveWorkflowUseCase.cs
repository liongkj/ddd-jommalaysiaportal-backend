using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class ApproveWorkflowUseCase : IApproveWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;

        public ApproveWorkflowUseCase(IWorkflowRepository workflowRepository)
        {
            _workfowRepository = workflowRepository;
        }
        public async Task<bool> Handle(WorkflowActionRequest message, IOutputPort<WorkflowActionResponse> outputPort)
        {
            throw new NotImplementedException();
            // try
            // {
            //var response = await _workfowRepository.GetAllWorkflowByStatusAsync(message.Status);
            //     outputPort.Handle(response);
            //     return response.Success;
            // }
            // catch (Exception e)
            // {
            //     outputPort.Handle(new WorkflowActionResponse(new List<string> { "Get Workflow Error" }, false, e.ToString()));
            //     return false;
            // }

        }
    }
}
