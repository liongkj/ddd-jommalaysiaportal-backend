using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace JomMalaysia.Core.Domain.Entities
{
    public class Category
    {
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public ICollection<Subcategory> Subcategory { get; set; }
        public string CategoryNameMs { get; private set; }
        public string CategoryNameZh { get; private set; }

        public Category()
        {
            Subcategory = new Collection<Subcategory>();
        }

        public void UpdateCategoryName(string language, string newName)
        {
            if (language == "en")
            {
                //TODO
            }
        }
    }
}