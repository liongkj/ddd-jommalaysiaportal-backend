using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Reject
{
    public class RejectWorkflowUseCase : IRejectWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;

        public RejectWorkflowUseCase(IWorkflowRepository workflowRepository)
        {
            _workfowRepository = workflowRepository;
        }
        public async Task<bool> Handle(WorkflowActionRequest message, IOutputPort<WorkflowActionResponse> outputPort)
        {
            var responder = new User
            {
                Role = "admin"
            };


            try
            {
                var getWorkflowResponse = await _workfowRepository.GetWorkflowByIdAsync(message.WorkflowId);
                if (!getWorkflowResponse.Success)
                {
                    outputPort.Handle(new WorkflowActionResponse(getWorkflowResponse.Errors, false, getWorkflowResponse.Message));
                    return false;
                }
                Workflow RejectedWorkflow = getWorkflowResponse.Workflow;
                var RejectedWorkflowCanProceed = responder.ApproveRejectWorkflow(RejectedWorkflow, message.Action, message.Comments);

                if (!RejectedWorkflowCanProceed)
                {
                    if (RejectedWorkflow.Responder != null) //not enought
                    {
                        outputPort.Handle(new WorkflowActionResponse(new List<string> { "User do not have enough authority" }));
                        return false;
                    }

                    if (RejectedWorkflow.Status.Equals(WorkflowStatusEnum.Rejected))
                    {
                        outputPort.Handle(new WorkflowActionResponse(new List<string> { "Could not proceed" }, false, "The selected workflow is completed and has already been rejected"));
                        return false;
                    }

                    outputPort.Handle(new WorkflowActionResponse(new List<string> { "Error Rejecting Workflow" }));
                    return false;
                }



                var response = await _workfowRepository.UpdateAsync(RejectedWorkflow);

                outputPort.Handle(response);
                return response.Success;
            }
            catch (Exception e)
            {
                outputPort.Handle(new WorkflowActionResponse(new List<string> { "Reject Workflow Error" }, false, e.ToString()));
                return false;
            }

        }
    }
}
