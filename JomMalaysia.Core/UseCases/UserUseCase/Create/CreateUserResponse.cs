using JomMalaysia.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.UseCases.UserUseCase.Create
{
    public class CreateUserResponse : UseCaseResponseMessage
    {


        public string Status { get; set; }

        public CreateUserResponse()
        {

        }

        public CreateUserResponse(string status, bool success = false, string message = null) : base(success, message)
        {
            Status = status;
        }
    }
}
