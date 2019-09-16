using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase
{
    public class WorkflowActionResponse : UseCaseResponseMessage
    {
        public string WorkflowId { get; }
        public IEnumerable<string> Errors { get; }

        public WorkflowActionResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public WorkflowActionResponse(string Workflowid, bool success = false, string message = null) : base(success, message)
        {
            this.WorkflowId = WorkflowId;
        }
    }
}
