using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace JomMalaysia.Core.Interfaces
{
    public abstract class UseCaseResponseMessage
    {
        public bool Success { get; }
        public string Message { get; }
        public int? StatusCode { get; }


        protected UseCaseResponseMessage(bool success = false, string message = null, int? statusCode = null)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
        }
    }
}