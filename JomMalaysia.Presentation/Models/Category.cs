using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JomMalaysia.Presentation.Models
{
    public class Category
    {
        public String categoryId { get; set; }
        public String categoryName { get; set; }
        public String categoryNameMs { get; set; }
        public String categoryNameZh { get; set; }

        public Category category { get; set; }
        public List<Category> lstSubCategory { get; set; }

        public Boolean selected { get; set; }
    }
}
