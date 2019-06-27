using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryResponse : UseCaseResponseMessage
    {
        public Category Category { get; }
        public IEnumerable<string> Errors { get; }

        public GetCategoryResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetCategoryResponse(Category Category, bool success = false, string message = null) : base(success, message)
        {
            this.Category = Category;
        }
    }
}
