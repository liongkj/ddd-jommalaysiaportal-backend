
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Api.UseCases.Categories.GetCategory
{
    public sealed class GetAllCategoryPresenter : IOutputPort<GetAllCategoryResponse>
    {
        public JsonContentResult ContentResult { get; }

        public GetAllCategoryPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetAllCategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
