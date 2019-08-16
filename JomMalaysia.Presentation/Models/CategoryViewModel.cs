using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Presentation.Gateways.Category;

namespace JomMalaysia.Presentation.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel() { }
        [Required]
        [StringLength(10, ErrorMessage = "{0} should not exceed 10 characters.")]
        [Display(Name = "Category Code")]
        public string CategoryCode { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Category Name")]
        public string CategoryName { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "Name Kategori")]
        public string CategoryNameMs { get; set; }

        [Required]
        [StringLength(500)]
        [Display(Name = "名称")]
        public string CategoryNameZh { get; set; }

        //[Required(ErrorMessage = "An Album Title is required")]
        //[DisplayFormat(ConvertEmptyStringToNull = false)]
        //[StringLength(160, MinimumLength = 2)]
        public string ParentCategory { get; set; }
        public string Subcategory { get; set; }
        public List<CategoryViewModel> LstSubCategory { get; set; }

        public bool IsDeleted { get; set; }
        public bool IsSelected { get; set; }

    }
}
