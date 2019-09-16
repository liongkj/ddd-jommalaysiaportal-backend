using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.Domain.Enums;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Approve
{
    public class ApproveWorkflowUseCase : IApproveWorkflowUseCase
    {
        private readonly IWorkflowRepository _workfowRepository;
        private readonly IListingRepository _listingRepository;

        public ApproveWorkflowUseCase(IWorkflowRepository workflowRepository, IListingRepository listingRepository)
        {
            _workfowRepository = workflowRepository;
            _listingRepository = listingRepository;
        }
        public async Task<bool> Handle(WorkflowActionRequest message, IOutputPort<WorkflowActionResponse> outputPort)
        {
            // throw new NotImplementedException();
            //TODO get current signed in user
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

                var ApprovedWorkflow = responder.ApproveRejectWorkflow(getWorkflowResponse.Workflow, message.Action, message.Comments);

                if (ApprovedWorkflow == null)
                {
                    outputPort.Handle(new WorkflowActionResponse(new List<string> { "Error Approving Workflow" }));
                    return false;
                }
                if (ApprovedWorkflow.Status.Equals(WorkflowStatusEnum.Completed))
                {
                    outputPort.Handle(new WorkflowActionResponse(new List<string> { "The selected workflow has been already been completed" }));
                    return false;
                }

                var response = await _workfowRepository.UpdateAsync(ApprovedWorkflow);
                //TODO update listing if published logic
                outputPort.Handle(response);
                return response.Success;
            }
            catch (Exception e)
            {
                outputPort.Handle(new WorkflowActionResponse(new List<string> { "Approve Workflow Error" }, false, e.ToString()));
                return false;
            }

        }
    }
}
