using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase
{
    public class WorkflowActionRequest : IUseCaseRequest<WorkflowActionResponse>
    {
        public WorkflowActionRequest(string workflowId, string action)
        {
            WorkflowId = workflowId;
            Action = action;
        }

        public string WorkflowId { get; set; }
        public string Action { get; set; }
    }
}