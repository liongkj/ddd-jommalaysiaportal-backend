
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
{
    public class GetCategoryByIdRequest : IUseCaseRequest<GetCategoryResponse>
    {

        public string Id { get; }
        public GetCategoryByIdRequest(string Id)
        {
            this.Id = Id;

        }
    }
}
