using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetWorkflowRequest : IUseCaseRequest<GetWorkflowResponse>
    {
        public string WorkflowId;

        public GetWorkflowRequest(string workflowId)
        {
            WorkflowId = workflowId;
        }
    }
}
