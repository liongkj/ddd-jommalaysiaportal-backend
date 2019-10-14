
using JomMalaysia.Core.Domain.Entities;

using JomMalaysia.Core.Interfaces;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryRequest : IUseCaseRequest<UpdateCategoryResponse>
    {

        public UpdateCategoryRequest(string id, Category Updated)
        {
            this.Updated = Updated;

            this.id = id;
        }



        public Category Updated { get; }
        public string id { get; }


    }
}