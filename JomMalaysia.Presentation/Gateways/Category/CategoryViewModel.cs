using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JomMalaysia.Presentation.Gateways.Category
{
    [JsonObject]
    public class CategoryViewModel
    {
        
        [Display(Name = "Category Id")]
        public string CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Display(Name = "Name Kategori")]
        public string CategoryNameMs { get; set; }

        [Display(Name = "名称")]
        public string CategoryNameZh { get; set; }
        
    }
}
