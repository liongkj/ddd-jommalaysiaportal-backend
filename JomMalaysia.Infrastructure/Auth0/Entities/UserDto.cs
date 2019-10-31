using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Infrastructure.Auth0.Entities
{
    public class UserDto
    {
        // public string user_id { get; set; }

        public string email { get; set; }
        // public string role { get; set; }

        public string username { get; set; }


        public string name { get; set; }

        public bool verify_email { get; set; }

        public string connection { get; set; } = "Username-Password-Authentication";

        public string password { get; set; } = "P@$$w0rd123";
    }
}
