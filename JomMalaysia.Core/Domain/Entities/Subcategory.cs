using System.Collections.Generic;
using System.Reflection.Metadata;
namespace JomMalaysia.Core.Domain.Entities
{
    public class Subcategory
    {
        public string SubcategoryId { get; set; }
        public string SubcategoryName { get; set; }
        public List<Listing> Listings { get; set; }
    }
}