using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Delete
{
    public class DeleteCategoryRequest : IUseCaseRequest<DeleteCategoryResponse>
    {
        public string Id { get; set; }
    }
}