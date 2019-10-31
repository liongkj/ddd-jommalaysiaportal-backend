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
        public UserViewModel Data { get; }
        public IEnumerable<string> Errors { get; }

        public GetUserResponse(List<string> errors, bool success = false, string message = null, int? statusCode = null) : base(success, message, statusCode)
        {
            Errors = errors;
        }

        public GetUserResponse(User user, bool success = false, string message = null) : base(success, message)
        {
            User = user;
        }

        public GetUserResponse(UserViewModel user, bool success = false, string message = null) : base(success, message)
        {
            Data = user;
        }
    }
}
