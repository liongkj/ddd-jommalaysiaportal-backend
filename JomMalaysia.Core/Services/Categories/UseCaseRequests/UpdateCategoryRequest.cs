
using JomMalaysia.Core.Domain.Entities;

using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
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