using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Create
{
    public class CreateUserRequest  : IUseCaseRequest<CreateUserResponse>
    {
        public User user { get; set; }

        public CreateUserRequest(User _user)
        {
            user = _user;
        }
    }
}
