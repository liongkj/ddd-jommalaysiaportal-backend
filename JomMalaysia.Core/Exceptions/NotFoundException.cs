using System;

namespace JomMalaysia.Core.Exceptions
{
    public class NotFoundException : Exception
    {

        public NotFoundException(string message)
            : base($"Record not found for " + message)
        {

        }

    }
}