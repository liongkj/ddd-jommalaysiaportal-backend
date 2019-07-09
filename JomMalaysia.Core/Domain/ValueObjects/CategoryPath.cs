using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Domain.ValueObjects
{
    public class CategoryPath : ValueObjectBase
    {
        public const char Dlm = ',';
        public string Category { get; private set; }
        public string Subcategory { get; private set; }
        private CategoryPath() { }
        public static CategoryPath For(string CategoryString)
        {
            var Cat = new CategoryPath();
            
            try
            {
                var index = CategoryString.IndexOf(Dlm, StringComparison.Ordinal);
                Cat.Category = CategoryString.Substring(0, index);
                Cat.Subcategory = CategoryString.Substring(index + 1);

            }
            catch (Exception e) { }
            return Cat;
        }

        public static explicit operator CategoryPath(string CategoryString)
        {
            return For(CategoryString);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new NotImplementedException();
        }
    }
}
