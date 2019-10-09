using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetAllCategoryResponse : UseCaseResponseMessage
    {
        public List<Category> Data { get; }
        public IEnumerable<string> Errors { get; }

        public GetAllCategoryResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetAllCategoryResponse(List<Category> Categories, bool success = false, string message = null) : base(success, message)
        {
            Data = Categories;
        }
    }
}
