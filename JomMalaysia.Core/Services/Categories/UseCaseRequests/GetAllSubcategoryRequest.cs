using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
{
    public class GetAllSubcategoryRequest : IUseCaseRequest<GetAllSubcategoryResponse>
    {

        public string Id { get; }
        public GetAllSubcategoryRequest(string Id)
        {
            this.Id = Id;

        }
    }
}
