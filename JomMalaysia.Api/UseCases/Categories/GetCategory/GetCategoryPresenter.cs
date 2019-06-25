
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Api.UseCases.Categories.GetCategory
{
    public sealed class GetCategoryPresenter : IOutputPort<GetCategoryResponse>,
    IOutputPort<GetAllSubcategoryResponse>
    {
        public JsonContentResult ContentResult { get; }

        public GetCategoryPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetCategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }

        public void Handle(GetAllSubcategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
