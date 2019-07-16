
using JomMalaysia.Core.Domain.Entities;

using JomMalaysia.Core.Interfaces;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryRequest : IUseCaseRequest<UpdateCategoryResponse>
    {
        public UpdateCategoryRequest(string CategoryName, Category Updated)
        {
            this.CategoryName = CategoryName;
            this.Updated = Updated;
        }
        public string CategoryName { get; }
        public Category Updated { get; }


    }
}