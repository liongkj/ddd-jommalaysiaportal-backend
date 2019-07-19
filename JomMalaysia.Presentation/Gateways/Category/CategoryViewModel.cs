using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Gateways.Category
{
    public class CategoryViewModel
    {

        [Display(Name = "Category Id")]
        public String CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public String CategoryName { get; set; }

        [Display(Name = "Name Kategori")]
        public String CategoryNameMs { get; set; }

        [Display(Name = "名称")]
        public String CategoryNameZh { get; set; }
        
    }
}
