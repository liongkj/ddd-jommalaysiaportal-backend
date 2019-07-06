using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Models
{
    public class User
    {
        [Display(Name="Email Address")]
        public String email { get; set; }

        [Display(Name = "Password")]
        public String password { get; set; }

        [Display(Name = "ReturnURL")]
        public String returnURL { get; set; }
    }
}
