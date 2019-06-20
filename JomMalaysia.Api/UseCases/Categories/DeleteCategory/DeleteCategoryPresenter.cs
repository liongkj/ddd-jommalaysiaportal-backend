
using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Api.UseCases.Categories.DeleteCategory
{
    public sealed class DeleteCategoryPresenter : IOutputPort<DeleteCategoryResponse>
    {
        public JsonContentResult ContentResult { get; }

        public DeleteCategoryPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(DeleteCategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
