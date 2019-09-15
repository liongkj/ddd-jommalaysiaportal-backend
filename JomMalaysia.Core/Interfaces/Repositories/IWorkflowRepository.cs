using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Get;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IWorkflowRepository
    {
        Task<CreateWorkflowResponse> CreateWorkflowAsyncWithSession(Workflow workflows, IClientSessionHandle session);
        GetAllWorkflowResponse FindByListing(List<string> listingIds, WorkflowStatusEnum workflowStatus);
        GetWorkflowResponse GetWorkflowById(string workflowId);
        Task<GetAllWorkflowResponse> GetAllWorkflowByStatusAsync(WorkflowStatusEnum status, int counterpage = 10, int page = 0);
    }
}
