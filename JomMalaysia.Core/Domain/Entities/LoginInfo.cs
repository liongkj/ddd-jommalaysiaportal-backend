using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.Entities
{
    public class LoginInfo
    {
        public string userId { get; set; }

        public string name { get; set; }

        public List<string> scope { get; set; }

        public string role { get; set; }
    }
}
