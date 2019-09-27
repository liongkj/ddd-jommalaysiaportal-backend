using System;
using System.Collections.Generic;
using System.Text;
using JomMalaysia.Core.Domain.ValueObjects;
using Xunit;

namespace JomMalaysia.Test.Core.ValueObject
{
    public class CategoryPathTest
    {
        [Fact]
        public void ExtractSubcategoryPathTest()
        {
            //given
            string cp = ",restaurant,chinese-muslim,";
            CategoryPath categorypath = (CategoryPath)(cp);
            //expect
            string category = "restaurant";
            string subcategory = "chinese muslim";


            Assert.Equal(category, categorypath.Category);
            Assert.Equal(subcategory, categorypath.Subcategory);
        }

        [Fact]
        public void ExtractCategoryPathTest()
        {
            //given
            string path = ",restaurant,";
            CategoryPath categorypath = (CategoryPath)(path);
            string category = "restaurant";



            Assert.Equal(category, categorypath.Category);
            Assert.Null(categorypath.Subcategory);
        }

        [Fact]
        public void GenerateSlugTest()
        {
            //given
            string category = "restaurant";
            string subcategory = "Chinese mUsLim";
            CategoryPath categorypath = new CategoryPath(category, subcategory);
            categorypath.ToString();
            Assert.Equal("restaurant", categorypath.Category);
            Assert.Equal("chinese-muslim", categorypath.Subcategory);

        }

        [Fact]
        public void GenerateCategoryPathTest()
        {
            //given
            string category = "restaurant";

            CategoryPath categorypath = new CategoryPath(category, "");

            Assert.Equal(",restaurant,", categorypath.ToString());

        }

        [Fact]
        public void GenerateSubcategoryPathTest()
        {
            //given
            string category = "restaurant";
            string subcategory = "chinese muslim";
            CategoryPath categorypath = new CategoryPath(category, subcategory);

            Assert.Equal(",restaurant,chinese-muslim,", categorypath.ToString());

        }
    }
}
