using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.UseCases.UserUseCase.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JomMalaysia.Api.UseCases.User
{
    public sealed class UserPresenter : IOutputPort<UseCaseResponseMessage>
    {
        public JsonContentResult ContentResult { get; }

        public UserPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(UseCaseResponseMessage response)
        {
            if (!response.StatusCode.HasValue)
                ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            else ContentResult.StatusCode = response.StatusCode;
            ContentResult.Content = JsonSerializer.SerializeObject(response);
        }
    }
}
