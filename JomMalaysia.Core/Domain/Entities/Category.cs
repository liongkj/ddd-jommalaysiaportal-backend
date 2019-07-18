
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



        public void CreateCategoryPath(string category, string sub = null, bool IsUpdateCategoryOperation = false)
        {
            //if is category
            //create new path
            if (category == null)
            { //if is parent
                CreateParentPath(sub);
            }
            //if is subcategory, create new subpath with changed category
            else //is subcategory
            {
                CreateSubPath(category, sub);
            }


        }

        public bool UpdateCategoryIsSuccess(Category updated, bool IsUpdateCategoryOperation = true)
        {
            if (updated == null)
            {
                throw new ArgumentNullException(nameof(updated));
            }
            var oldCategoryName = CategoryName;
            //1. update to new name
            UpdateName(updated);

            //2. create new category path
            if (IsUpdateCategoryOperation)//if update category operation
            {
                if (CategoryPath.Subcategory == null)
                    //update category path
                    CreateCategoryPath(updated.CategoryName);
                //update subcategory path
                else CreateCategoryPath(updated.CategoryName, CategoryPath.Subcategory);
            }
            else //IsupdateSubcategoryOperation
            {
                CreateCategoryPath(CategoryPath.Category, updated.CategoryName);
            }


            return true;


            //TODO update name logic
        }

        //update name
        //update category path


        public List<Category> UpdateSubcategories(List<Category> subcategories, Category Updated)
        {
            if (subcategories.Count > 0)
            {
                List<Category> UpdatedSubs = new List<Category>();
                foreach (var sub in subcategories)
                {
                    sub.CreateCategoryPath(Updated.CategoryPath.Category, sub.CategoryPath.Subcategory, false);
                    UpdatedSubs.Add(sub);

                }
                return UpdatedSubs;
            }
            return subcategories;
        }

        #region private methods
        private void CreateParentPath(string category)
        {
            CategoryPath = new CategoryPath(category, null);
        }

        private void CreateSubPath(string category, string sub)
        {

            CategoryPath = new CategoryPath(category, sub);


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
