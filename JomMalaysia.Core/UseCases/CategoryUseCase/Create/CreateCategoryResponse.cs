using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Create
{
    public class CreateCategoryResponse : UseCaseResponseMessage
    {
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

        public CreateCategoryResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public CreateCategoryResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
    }
}
