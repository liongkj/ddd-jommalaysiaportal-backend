
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Category
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

        public bool HasSubcategories(List<Category> subcategories)
        {
            if (subcategories
                != null)
            {
                return subcategories.Count > 0;
            }

            return false;
        }

        public bool HasDuplicate(List<Category> categories)
        {
            if (categories != null)
            {
                foreach (var c in categories)
                {
                    if (c.CategoryPath.Equals(CategoryPath))
                    {
                        return true;
                    }
                }
            }
            return false;
        }



        public void CreateCategoryPath(string parent)
        {
            if (parent == null)
            { //if is parent
                CreateParentPath();
            }
            else //is subcategory
            {
                CreateSubPath(parent);
            }
        }

        public bool UpdateNameIsSuccess(Category updated)
        {
            if (updated == null)
            {
                throw new ArgumentNullException(nameof(updated));
            }
            UpdateName(updated);

            CreateCategoryPath(CategoryPath.Subcategory == null ? null:updated.CategoryName );

            return true;
            //TODO update name logic
        }

        public List<Category> UpdateSubcategories(List<Category> subcategories,Category Updated)
        {
            if (subcategories.Count > 0)
            {
                List<Category> UpdatedSubs = new List<Category>();
                foreach (var sub in subcategories)
                {
                    sub.CreateCategoryPath(Updated.CategoryPath.Category);
                    UpdatedSubs.Add(sub);
                    
                }
                return UpdatedSubs;
            }
            return subcategories;
        }

        #region private methods
        private void CreateParentPath()
        {
            CategoryPath = new CategoryPath(CategoryName, null);
        }

        private void CreateSubPath(string parent)
        {
            CategoryPath = new CategoryPath(parent, CategoryName);

        }

        private void UpdateName(Category updated)
        {
            CategoryName = updated.CategoryName;
            CategoryNameMs = updated.CategoryNameMs;
            CategoryNameZh = updated.CategoryNameZh;
        }
        #endregion

    }
}
