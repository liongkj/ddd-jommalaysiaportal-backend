using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.ListingUseCase
{
    public class ListingWorkflowResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public ListingWorkflowResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public ListingWorkflowResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}