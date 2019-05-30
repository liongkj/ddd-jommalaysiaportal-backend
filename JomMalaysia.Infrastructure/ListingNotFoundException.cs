using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Infrastructure
{
    public class ListingNotFoundException : InfrastructureException
    {
        internal ListingNotFoundException(string message)
            : base(message)
        { }
    }
}