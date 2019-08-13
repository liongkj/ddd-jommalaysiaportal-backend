using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Gateways.Category;

namespace JomMalaysia.Presentation.Models
{
    public class Category
    {
        public Category() { }
        [Required]
        [StringLength(10, ErrorMessage ="{0} should not exceed 10 characters.")]
        [Display(Name="Category Code")]
        public String categoryCode { get; set; }

        [Required]
        [StringLength(500)] 
        [Display(Name = "Category Name")]
        public String categoryName { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Category Name Ms")]
        public String categoryNameMs { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Category Name Zh")]
        public String categoryNameZh { get; set; }

        //[Required(ErrorMessage = "An Album Title is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //[StringLength(160, MinimumLength = 2)]
        public Category category { get; set; }
        public List<Category> lstSubCategory { get; set; }

        public Boolean isDeleted { get; set; }
        public Boolean selected { get; set; }

    }
}
