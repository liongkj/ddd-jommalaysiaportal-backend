using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core
{
    public class ApplicationException : Exception
    {
        internal ApplicationException(string businessMessage)
               : base(businessMessage)
        {
        }
    }
}