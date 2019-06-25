using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
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
