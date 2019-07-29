using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase.Create
{
    public class CreateWorkflowResponse: UseCaseResponseMessage
    {
        public string Count { get; }
        public IEnumerable<string> Errors { get; }

        public CreateWorkflowResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public CreateWorkflowResponse(string count, bool success = false, string message = null) : base(success, message)
        {
            Count = count;
        }
    }
}
