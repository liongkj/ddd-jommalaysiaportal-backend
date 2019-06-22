using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
{
    public class GetCategoryByNameRequest : IUseCaseRequest<GetCategoryResponse>
    {
        public string Name { get; set; }

        public GetCategoryByNameRequest(string name)
        {
            Name = name;
        }
    }
}