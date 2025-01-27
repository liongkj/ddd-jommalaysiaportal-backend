﻿using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryResponse : UseCaseResponseMessage
    {
        public DeleteCategoryResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public DeleteCategoryResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            Id = id;
        }
        public string Id { get; }
        public IEnumerable<string> Errors { get; }

    }
}