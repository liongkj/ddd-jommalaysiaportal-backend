using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Models
{
    public class Merchant
    {
        [Display(Name = "SSMID")]
        public String ssmID { get; set; }

        [Display(Name = "Company Name")]
        public String companyName { get; set; }

        [Display(Name = "Registered Company Address")]
        public String companyAddress { get; set; }

        [Display(Name = "Merchant Name")]
        public String merchantName { get; set; }

        [Display(Name = "Contact Number")]
        public String contactNumber { get; set; }

        public Boolean selected { get; set; }
    }
}
