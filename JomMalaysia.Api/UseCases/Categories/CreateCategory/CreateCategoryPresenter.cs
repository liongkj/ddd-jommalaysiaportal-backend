
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Categories.CreateCategory
{
    public sealed class CreateCategoryPresenter : IOutputPort<CreateCategoryResponse>
    {
        public JsonContentResult ContentResult { get; }

        public CreateCategoryPresenter()
        {
            ContentResult = new JsonContentResult();
        }


        public void Handle(CreateCategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
