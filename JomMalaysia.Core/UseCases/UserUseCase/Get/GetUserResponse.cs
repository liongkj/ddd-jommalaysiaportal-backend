using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Get
{
    public class GetUserResponse : UseCaseResponseMessage
    {
        public User User { get; }
        public string Error { get; }

        public GetUserResponse(string errors, bool success = false, string message = null) : base(success, message)
        {
            Error = errors;
        }

        public GetUserResponse(User user, bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }
    }
}
