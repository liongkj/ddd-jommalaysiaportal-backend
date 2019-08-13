using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Get
{
    public class GetWorkflowResponse : UseCaseResponseMessage
    {
        public Workflow Workflow { get; }
        

        public IEnumerable<string> Errors { get; }

        public GetWorkflowResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetWorkflowResponse(Workflow Workflow, bool success = false, string message = null) : base(success, message)
        {
            this.Workflow = Workflow;
           
        }
    }
}