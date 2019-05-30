using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Infrastructure
{
    public class MerchantNotFoundException : InfrastructureException
    {
        internal MerchantNotFoundException(string message)
            : base(message)
        { }
    }
}