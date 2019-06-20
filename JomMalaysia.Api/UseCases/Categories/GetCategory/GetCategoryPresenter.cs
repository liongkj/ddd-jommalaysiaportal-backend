
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Categories.GetCategory
{
    public sealed class GetCategoryPresenter : IOutputPort<GetCategoryResponse>
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
    }
}
