using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Exceptions;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Approve
{
    public class ApproveWorkflowUseCase : IApproveWorkflowUseCase
    {
        private readonly IWorkflowRepository _workflowRepository;
        private readonly IListingRepository _listingRepository;
        private readonly ILoginInfoProvider _loginInfo;
        private readonly IMongoDbContext _transaction;
        public ApproveWorkflowUseCase(IWorkflowRepository workflowRepository, IListingRepository listingRepository, ILoginInfoProvider loginInfo, IMongoDbContext transaction)
        {
            _workflowRepository = workflowRepository;
            _listingRepository = listingRepository;
            _loginInfo = loginInfo;
            _transaction = transaction;
        }
        public async Task<bool> Handle(WorkflowActionRequest message, IOutputPort<WorkflowActionResponse> outputPort)
        {
            var responder = _loginInfo.AuthenticatedUser();
            if (responder == null) throw new NotAuthorizedException();
            try
            {
                //get workflow details from db
                var getWorkflowResponse = await _workflowRepository.GetWorkflowByIdAsync(message.WorkflowId);
                if (!getWorkflowResponse.Success)
                {
                    outputPort.Handle(new WorkflowActionResponse(getWorkflowResponse.Errors, false, getWorkflowResponse.Message));
                    return false;
                }
                var ApprovedWorkflow = getWorkflowResponse.Workflow;
                //check user priveledge
                var ApprovedWorkflowCanProceed = responder.ApproveRejectWorkflow(ApprovedWorkflow, message.Action, message.Comments);

                if (!ApprovedWorkflowCanProceed)
                {//handle approve workflow error
                    if (ApprovedWorkflow.Responder != null) //not enough priviledge
                    {
                        outputPort.Handle(new WorkflowActionResponse(new List<string> { "User do not have enough authority" }));
                        return false;
                    }
                    if (ApprovedWorkflow.Status.Equals(WorkflowStatusEnum.Completed))
                    {
                        outputPort.Handle(new WorkflowActionResponse(new List<string> { "Could not proceed" }, false, "The selected workflow has been already been completed and the changes will be live soon"));
                        return false;
                    }
                    outputPort.Handle(new WorkflowActionResponse(new List<string> { "Error Approving Workflow" }));
                    return false;
                }
                if (ApprovedWorkflow.IsCompleted())
                {
                    return await GoLiveOperation.GoLive(_transaction, outputPort, _workflowRepository, _listingRepository, ApprovedWorkflow);
                }
                else
                {
                    var response = await _workflowRepository.UpdateAsync(ApprovedWorkflow);
                    outputPort.Handle(response);
                    return response.Success;
                }
                //save workflow info



            }
            catch (Exception e)
            {
                outputPort.Handle(new WorkflowActionResponse(new List<string> { "Approve Workflow Error" }, false, e.ToString()));
                return false;
            }

        }
    }
}
