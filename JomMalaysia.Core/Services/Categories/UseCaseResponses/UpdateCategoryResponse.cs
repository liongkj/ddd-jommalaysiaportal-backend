using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.Categories.UseCaseResponses
{
    public class UpdateCategoryResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public UpdateCategoryResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UpdateCategoryResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}