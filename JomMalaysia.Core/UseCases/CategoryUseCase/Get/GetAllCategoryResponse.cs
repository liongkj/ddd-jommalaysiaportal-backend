﻿using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CategoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetAllCategoryResponse : UseCaseResponseMessage
    {
        public List<Category> Categories { get; }
        public List<CategoryViewModel> Data { get; }
        public IEnumerable<string> Errors { get; }

        public GetAllCategoryResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetAllCategoryResponse(List<Category> categories, bool success = false, string message = null) : base(success, message)
        {
            Categories = categories;
        }
        public GetAllCategoryResponse(List<CategoryViewModel> Categories, bool success = false, string message = null) : base(success, message)
        {
            Data = Categories;
        }
    }
}
