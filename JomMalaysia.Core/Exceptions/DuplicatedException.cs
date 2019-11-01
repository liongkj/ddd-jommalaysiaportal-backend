using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace JomMalaysia.Core.Exceptions
{
    public class DuplicatedException : Exception
    {
        public string Error { get; }


        public DuplicatedException(string error, string msg)
            : base(msg)
        {
            Error = error;

        }

    }
}
