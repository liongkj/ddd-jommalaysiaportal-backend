﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.UseCases.ListingUseCase;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.WorkflowUseCase;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IWorkflowRepository
    {
        Task<NewWorkflowResponse> CreateWorkflowAsyncWithSession(Workflow workflows, IClientSessionHandle session);
        //GetAllWorkflowResponse FindByListing(List<string> listingIds, WorkflowStatusEnum workflowStatus);
        Task<GetWorkflowResponse> GetWorkflowByIdAsync(string workflowId);
        Task<GetAllWorkflowResponse> GetAllWorkflowByStatusAsync(WorkflowStatusEnum status, int counterpage = 10, int page = 0);
        Task<WorkflowActionResponse> UpdateAsync(Workflow updateddWorkflow, IClientSessionHandle sessionHandle = null);
        Task<bool> GetPendingWorkflowForListing(string listingId);
    }
}
