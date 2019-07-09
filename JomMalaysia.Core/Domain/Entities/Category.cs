using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Category:ICategory
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public CategoryPath CategoryPath{ get; set; }

        public Category(string categoryName, string categoryNameMs, string categoryNameZh, CategoryPath CategoryPath)
        {
            CategoryName = categoryName;
            CategoryNameMs = categoryNameMs;
            CategoryNameZh = categoryNameZh;
            this.CategoryPath = CategoryPath;
           
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
