using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class CategoryPath : ValueObjectBase
    {
        const char DLM = ',';
        
        public string Category { get; private set; }
        public string Subcategory { get; private set; }

        
        public CategoryPath(string category, string sub)
        {
            if (string.IsNullOrEmpty(category))
            {
                throw new ArgumentException("message", nameof(category));
            }
            Category = category.ToLower();
            if (sub != null)
            {
                Subcategory = sub.ToLower();
            }

            
            
        }

        private CategoryPath() { }


        public static CategoryPath For(string CategoryPathString)
        {
            if (string.IsNullOrWhiteSpace(CategoryPathString))
            {
                throw new ArgumentException("Category and Subcategory cannot be null", nameof(CategoryPathString));
            }

            var Cat = new CategoryPath();
            try
            {
                CategoryPathString = Regex.Replace(CategoryPathString, ",", " ").Trim();
                var strings = CategoryPathString.Split(" ");
                Cat.Category = GeneratePhrase(strings[0]);
                Cat.Subcategory = GeneratePhrase(strings[1]);
            }
            catch (Exception) { }
            return Cat;
        }

        public static explicit operator CategoryPath(string CategoryString)
        {
            return For(CategoryString);
        }





        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(DLM);
            if (!string.IsNullOrEmpty(Category) )
            {
                var cat = GenerateSlug(Category);
                Category = cat;
                builder.Append(cat);
                if (!string.IsNullOrEmpty(Subcategory))
                {
                    builder.Append(DLM);
                    var sub = GenerateSlug(Subcategory);
                    Subcategory = sub;
                    builder.Append(sub);
                   
                }
                
            }
            builder.Append(DLM);

            return builder.ToString();
        }

        private string GenerateSlug(string phrase)
        {
            string str = phrase.ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        private static string GeneratePhrase(string slug)
        {
            string str = slug;
            str = Regex.Replace(str, "-", " "); // hyphens -> space
            // cut and trim 
            
            
            return str;
        }

        private string RemoveAccent(string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Category;
            yield return Subcategory;
        }

        public bool SameAs(CategoryPath obj)
        {
            return 
                   Category.Equals(obj.Category )&&
                   Subcategory.Equals(obj.Subcategory);
        }
    }
}
