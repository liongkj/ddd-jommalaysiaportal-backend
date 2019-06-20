using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Collection<Subcategory> Subcategories { get; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }

        public Category()
        {
            Subcategories = new Collection<Subcategory>();
        }

        public Category(string categoryName, string categoryNameMs, string categoryNameZh)
        {
            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
            Subcategories = new Collection<Subcategory>();
        }

        public Subcategory CreateSubCategory(string SubcategoryName, string CategoryNameMs, string CategoryNameZh)
        {
            var sub = new Subcategory(this, SubcategoryName, CategoryNameMs, CategoryNameZh);
            Subcategories.Add(sub);
            return sub;
        }
    }
}
