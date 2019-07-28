using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.WorkflowUseCase.Create;

namespace JomMalaysia.Core.Interfaces.Repositories
{
    public interface IWorkflowRepository
    {
        CreateWorkflowResponse CreateWorkflow(List<Workflow> workflows);
        GetAllWorkflowResponse FindByListing(List<string> listingIds, WorkflowStatusEnum workflowStatus);
    }
}
