using JomMalaysia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Delete
{
    public class DeleteUserResponse : UseCaseResponseMessage
    {
        public string UserId { get; }

        public IEnumerable<string> Errors { get; }

        public DeleteUserResponse()
        {

        }
        public DeleteUserResponse(IEnumerable<string> errors, bool success = false, string message = null, int? statusCode = null) : base(success, message, statusCode)
        {
            Errors = errors;
        }

        public DeleteUserResponse(string id, bool success = false, string message = null) : base(success, message)
        {
            UserId = id;
        }
    }
}
