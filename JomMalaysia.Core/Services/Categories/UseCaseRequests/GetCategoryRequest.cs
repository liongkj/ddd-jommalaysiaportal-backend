
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
{
    public class GetCategoryRequest : IUseCaseRequest<GetCategoryResponse>
    {
        public string Id { get; }

        public GetCategoryRequest(string CategoryId)
        {
            Id = CategoryId;
        }
    }
}
