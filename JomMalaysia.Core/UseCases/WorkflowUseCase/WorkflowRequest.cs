using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase
{
    public class WorkflowRequest : IUseCaseRequest<WorkflowActionResponse>
    {
        public string Comments { get; set; } = "";
    }
}