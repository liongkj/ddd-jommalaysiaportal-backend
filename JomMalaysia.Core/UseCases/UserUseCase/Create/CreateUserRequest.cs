using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Create
{
    public class CreateUserRequest : IUseCaseRequest<CreateUserResponse>
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}
