using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.Helper
{
    public class Auth0PagingHelper<T>
    {
        public int total { get; set; }

        public int start { get; set; }

        public int limit { get; set; }

        public int length { get; set; }

        public List<T> users { get; set; }
    }
}
