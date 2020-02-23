
using System;
using System.Collections.Generic;
using System.Linq;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Domain.Entities.Listings;
using JomMalaysia.Core.Domain.Enums;
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
        public CategoryType CategoryType { get; set; }
        public CategoryPath CategoryPath { get; set; }
        public Image CategoryThumbnail { get; set; }

        public Category()
        {

        }


        public Category(CategoryType categoryType, string categoryCode, string categoryName, string categoryNameMs,
            string categoryNameZh, Image image)
        {
            CategoryType = categoryType;
            CategoryCode = HandleCode(categoryCode, categoryName); ;
            CategoryName = categoryName.Trim().ToLower();
            CategoryNameMs = categoryNameMs.Trim().ToLower();
            CategoryNameZh = categoryNameZh.Trim().ToLower();
            CategoryThumbnail = image;
        }

        public Category(string categoryCode, string categoryName, string categoryNameMs, string categoryNameZh, Image image)
        {
            CategoryCode = HandleCode(categoryCode, CategoryName);
            CategoryName = categoryName.Trim().ToLower();
            CategoryNameMs = categoryNameMs.Trim().ToLower();
            CategoryNameZh = categoryNameZh.Trim().ToLower();
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
            return categories != null && categories.Any(c => c.CategoryPath.Equals(CategoryPath));
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

        public Dictionary<string, CategoryPath> UpdateListings(List<Listing> toBeUpdateListings, bool IsUpdateCategoryOperation)
        {
            Dictionary<string, CategoryPath> updatedListings = new Dictionary<string, CategoryPath>();
            foreach (var listing in toBeUpdateListings)
            {
                CategoryPath cp;
                cp = IsUpdateCategoryOperation ?
                    new CategoryPath(CategoryName, listing.Category.Subcategory)
                    : new CategoryPath(listing.Category.Category, CategoryName);
                cp.CategoryId = CategoryId;
                updatedListings.Add(listing.ListingId, cp);
            }
            return updatedListings;
        }

        public List<Category> UpdateCategory(Category updated, List<Category> ToBeUpdate = null, bool IsUpdateCategoryOperation = true)
        {
            List<Category> updatedCategories = new List<Category>();
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
                updatedCategories.Add(this);
                updatedCategories.AddRange(UpdateSubcategories(ToBeUpdate, this));
                return updatedCategories;
            }
            UpdateSubcategory(updated);
            return null;
        }

        private void UpdateImage(Category updated)
        {
            CategoryThumbnail = updated.CategoryThumbnail == null ? new Image() : new Image(updated.CategoryThumbnail.Url);
        }

        public bool IsCategory()
        {
            return CategoryPath.Subcategory == null;
        }

        #region private methods
        private static IEnumerable<Category> UpdateSubcategories(List<Category> subcategories, Category Updated)
        {
            if (subcategories.Count <= 0) return subcategories;
            var updatedSubs = new List<Category>();
            foreach (var sub in subcategories)
            {
                sub.CreateCategoryPath(Updated.CategoryPath.Category, sub.CategoryPath.Subcategory, false);
                updatedSubs.Add(sub);
            }
            return updatedSubs;
        }

        private string HandleCode(string categoryCode, string categoryName)
        {
            if (categoryCode != null) return categoryCode.Trim().ToUpper();

            var index = categoryName.Length >= 5 ? 5 : categoryName.Length;
            return categoryName.Substring(0, index).ToUpper();

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

    public enum CategoryType
    {
        Professional,
        Government,
        Private,
        Nonprofit,
        Attraction
    }
}
