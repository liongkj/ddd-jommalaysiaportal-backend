using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase.Get.Response;
using System.Net;

namespace JomMalaysia.Api.UseCases.User.GetUser
{
    public sealed class GetAllUserPresenter : IOutputPort<GetAllUserResponse>
    {
        public JsonContentResult ContentResult { get; }

        public GetAllUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(GetAllUserResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
