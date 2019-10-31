using System;
using System.Collections.Generic;

namespace JomMalaysia.Infrastructure.Auth0.Entities
{




    public class Authorization
    {
        public List<string> roles { get; set; }
        public List<string> permissions { get; set; }
    }

    public class AppMetadata
    {
        public Authorization authorization { get; set; }
    }

    public class Auth0User
    {
        public string email { get; set; }
        public string username { get; set; }

        public string name { get; set; }
        public string picture { get; set; }
        public string user_id { get; set; }


        public DateTime? last_login { get; set; }

        public AppMetadata app_metadata { get; set; }
    }
}