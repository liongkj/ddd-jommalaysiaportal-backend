using System.Collections.Generic;

namespace JomMalaysia.Infrastructure.Auth0
{
    public class Auth0RoleList
    {
        public List<Role> roles { get; set; }
        public class Role
        {
            public string _id { get; set; }

            public string name { get; set; }

        }
    }
}