using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Delete
{
    public class DeleteUserRequest : IUseCaseRequest<DeleteUserResponse>
    {
        public DeleteUserRequest(string userid)
        {
            Userid = userid;
        }

        public string Userid { get; }
    }
}
