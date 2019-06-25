using System.Net;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;

namespace JomMalaysia.Api.UseCases.Categories.UpdateCategory
{
    public class UpdateCategoryPresenter : IOutputPort<UpdateCategoryResponse>, IOutputPort<CreateSubcategoryResponse>,IOutputPort<DeleteSubcategoryResponse>
    {
        public JsonContentResult ContentResult { get; }

        public UpdateCategoryPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UpdateCategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }

        public void Handle(CreateSubcategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }

        public void Handle(DeleteSubcategoryResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
