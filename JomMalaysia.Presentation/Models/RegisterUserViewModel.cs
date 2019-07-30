using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Models
{
    public class RegisterUserViewModel
    {
        public string email { get; set; }

        public string phone_number { get; set; }

        public bool email_verified { get; set; }

        public string given_name { get; set; }

        public string family_name { get; set; }

        public bool verify_email { get; set; }
    }
}
