using System;

namespace JomMalaysia.Core.Exceptions
{
    public class MappingException : Exception
    {
        public MappingException(string msg)
            : base("Application Error at: " + msg)
        {

        }
    }
}