using JomMalaysia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Create
{
    public class CreateUserResponse : UseCaseResponseMessage
    {
        public string user_id { get; }

        public IEnumerable<string> Errors { get; }

        public CreateUserResponse()
        {

        }
        public CreateUserResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public CreateUserResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            user_id = id;
        }
    }
}
