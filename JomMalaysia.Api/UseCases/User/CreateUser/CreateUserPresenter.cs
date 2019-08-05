using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JomMalaysia.Api.UseCases.User.CreateUser
{
    public sealed class CreateUserPresenter : IOutputPort<CreateUserResponse>
    {
        public JsonContentResult ContentResult { get; }

        public CreateUserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(CreateUserResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
