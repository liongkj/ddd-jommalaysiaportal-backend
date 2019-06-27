
using JomMalaysia.Core.Domain.Entities;

using JomMalaysia.Core.Interfaces;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryRequest : IUseCaseRequest<UpdateCategoryResponse>
    {
        public UpdateCategoryRequest(string CategoryId, Category Updated)
        {
            this.CategoryId = CategoryId;
            this.Updated = Updated;
        }
        public string CategoryId { get; }
        public Category Updated { get; }


    }
}