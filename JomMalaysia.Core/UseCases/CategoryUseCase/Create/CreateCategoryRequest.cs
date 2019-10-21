﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Create
{
    public class CreateCategoryRequest : IUseCaseRequest<CreateCategoryResponse>
    {
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string CategoryNameMs { get; set; }
        public string CategoryNameZh { get; set; }
        public string ParentCategory { get; set; }
        public Image Image { get; set; }

        public CreateCategoryRequest(string categoryCode, string categoryName, string categoryNameMs, string categoryNameZh, Image image, string ParentCategory)
        {

            CategoryName = categoryName.Trim().ToLower();
            CategoryCode = handleCode(categoryCode, CategoryName);
            CategoryNameMs = categoryNameMs.Trim().ToLower();
            CategoryNameZh = categoryNameZh.Trim().ToLower();
            this.ParentCategory = ParentCategory;
            Image = image;
        }

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
    }
}
