using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase
{
    public class WorkflowActionRequest : IUseCaseRequest<WorkflowActionResponse>
    {


        public string WorkflowId { get; set; }
        public string Action { get; set; }
        public string Comments { get; set; }
    }
}