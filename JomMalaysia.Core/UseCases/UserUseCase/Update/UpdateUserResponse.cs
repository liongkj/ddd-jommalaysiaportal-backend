using JomMalaysia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Update
{
    public class UpdateUserResponse : UseCaseResponseMessage
    {
        public string UserId { get; }

        public IEnumerable<string> Errors { get; }

        public UpdateUserResponse()
        {

        }
        public UpdateUserResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public UpdateUserResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            UserId = id;
        }
    }
}
