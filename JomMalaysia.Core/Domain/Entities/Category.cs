
using System;
using System.Collections.Generic;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public CategoryPath CategoryPath { get; set; }
        public Image CategoryThumbnail { get; set; }

        public Category()
        {

        }


        public Category(string categoryCode, string categoryName, string categoryNameMs, string categoryNameZh, Image image)
        {
            CategoryCode = handleCode(categoryCode, CategoryName); ;
            CategoryName = categoryName.Trim().ToLower(); ;
            CategoryNameMs = categoryNameMs.Trim().ToLower(); ;
            CategoryNameZh = categoryNameZh.Trim().ToLower(); ;
            CategoryThumbnail = image;
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

        public Dictionary<string, string> UpdateListings(List<Listing> toBeUpdateListings, bool IsUpdateCategoryOperation)
        {
            Dictionary<string, string> UpdatedListings = new Dictionary<string, string>();
            foreach (var listing in toBeUpdateListings)
            {
                var converted = (IWithCategory)listing;
                CategoryPath cp;
                if (IsUpdateCategoryOperation)
                {
                    cp = new CategoryPath(this.CategoryName, converted.Category.Subcategory);

                }
                else
                {
                    cp = new CategoryPath(converted.Category.Category, this.CategoryName);
                }
                UpdatedListings.Add(converted.ListingId, cp.ToString());
            }
            return UpdatedListings;
        }

        public List<Category> UpdateCategory(Category updated, List<Category> ToBeUpdate = null, bool IsUpdateCategoryOperation = true)
        {
            List<Category> UpdatedCategories = new List<Category>();
            if (updated == null)
            {
                throw new ArgumentNullException(nameof(updated));
            }
            //1. update to new name
            UpdateName(updated);
            //2. Update image
            UpdateImage(updated);
            //3. create new category path
            if (IsUpdateCategoryOperation)//if update category operation
            {
                UpdateCategory(updated);
                UpdatedCategories.Add(this);
                UpdatedCategories.AddRange(UpdateSubcategories(ToBeUpdate, this));
                return UpdatedCategories;
            }
            else //IsupdateSubcategoryOperation
            {
                UpdateSubcategory(updated);
                return null;
            }


        }

        private void UpdateImage(Category updated)
        {
            if (updated.CategoryThumbnail == null)
                CategoryThumbnail = new Image();
            else
                CategoryThumbnail = new Image(updated.CategoryThumbnail.Url, updated.CategoryThumbnail.ThumbnailUrl);
        }

        public bool IsCategory()
        {
            return CategoryPath.Subcategory == null;
        }


        private List<Category> UpdateSubcategories(List<Category> subcategories, Category Updated)
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
        private string handleCode(string categoryCode, string categoryName)
        {
            if (categoryCode == null)
            {
                var index = categoryName.Length >= 5 ? 5 : categoryName.Length;
                return categoryName.Substring(0, index).ToUpper();
            }
            else
            {
                return categoryCode.Trim().ToUpper();
            }
        }
        private void UpdateCategory(Category updated)
        {
            if (CategoryPath.Subcategory == null)
                //update category path
                CreateCategoryPath(updated.CategoryName);
            //update subcategory path
            else CreateCategoryPath(updated.CategoryName, CategoryPath.Subcategory);
        }

        private void UpdateSubcategory(Category updated)
        {
            CreateCategoryPath(CategoryPath.Category, updated.CategoryName);
        }

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
            CategoryCode = updated.CategoryCode;

            CategoryName = updated.CategoryName;
            CategoryNameMs = updated.CategoryNameMs;
            CategoryNameZh = updated.CategoryNameZh;
            CategoryThumbnail = updated.CategoryThumbnail;
        }
        #endregion

    }
}
