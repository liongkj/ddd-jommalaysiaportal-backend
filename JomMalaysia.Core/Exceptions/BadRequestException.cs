using System;

namespace JomMalaysia.Core.Exceptions
{
    public class BadRequestException : Exception
    {

        public BadRequestException(string message)
            : base($"Validation failure at: {message}.")
        {

        }

    }
}