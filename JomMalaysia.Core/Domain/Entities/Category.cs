using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Category : ICategory
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public CategoryPath CategoryPath { get; set; }

        public Category(string categoryName, string categoryNameMs, string categoryNameZh, CategoryPath CategoryPath)
        {
            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
            this.CategoryPath = CategoryPath;
        }

        public Category(string categoryName, string categoryNameMs, string categoryNameZh)
        {
            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
        }

        public bool HasDuplicate(List<Category> categories)
        {
            foreach (var c in categories)
            {
                if (c.CategoryPath.Equals(CategoryPath))
                {
                    return true;
                }
            }
            return false;
        }

        internal void CreateCategoryPath(Category parent)
        {
            if (parent == null)
            { //if is parent
               IsParentCategory();
            }
            else //is subcategory
            {
                IsSubcategory(parent);
            }
        }

        

        private void IsParentCategory()
        {
            CategoryPath = new CategoryPath(CategoryName, null);
        }

        private void IsSubcategory(Category parent)
        {
            CategoryPath = new CategoryPath(parent.CategoryName, CategoryName);

        }

        public void UpdateName(string eng, string malay, string chinese)
        {
            if (string.IsNullOrWhiteSpace(eng))
            {
                throw new ArgumentException("message", nameof(eng));
            }

            if (string.IsNullOrWhiteSpace(malay))
            {
                throw new ArgumentException("message", nameof(malay));
            }

            if (string.IsNullOrWhiteSpace(chinese))
            {
                throw new ArgumentException("message", nameof(chinese));
            }
        }
    }
}
