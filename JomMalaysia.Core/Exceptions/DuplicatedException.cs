using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace JomMalaysia.Core.Exceptions
{
    public class DuplicatedException : Exception
    {
        private string Msg { get; set; }
        public DuplicatedException(string msg)
            : base(msg)
        {

            Msg = msg;
        }

    }
}
