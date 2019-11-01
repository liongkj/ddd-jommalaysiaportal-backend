using System;
using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Exceptions
{
    public class ExceptionResponse : UseCaseResponseMessage
    {
        public IEnumerable<string> Errors { get; set; }
        public ExceptionResponse(string message = null, int? statusCode = null) : base(false, message, statusCode)
        {

        }

        public ExceptionResponse(IEnumerable<string> errors, string message = null, int? statusCode = null) : base(false, message, statusCode)
        {
            Errors = errors;

        }
    }
}