using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Models
{
    public class Category
    {
        [Display(Name="Category ID")]
        public String categoryId { get; set; }
        [Display(Name = "Category Name")]
        public String categoryName { get; set; }

        [Display(Name = "Category Name Ms")]
        public String categoryNameMs { get; set; }

        [Display(Name = "Category Name Zh")]
        public String categoryNameZh { get; set; }

        public Category category { get; set; }
        public List<Category> lstSubCategory { get; set; }

        public Boolean isDeleted { get; set; }
        public Boolean selected { get; set; }
    }
}
