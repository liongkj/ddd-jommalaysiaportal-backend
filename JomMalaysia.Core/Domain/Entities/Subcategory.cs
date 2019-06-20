using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;

public class Subcategory
{
    internal Category Category { get; private set; }
    public Subcategory()
    {

    }
    internal Subcategory(Category category, string SubcategoryName, string SubcategoryNameMs, string SubcategoryNameZh)
    {
        this.Category = category;
        this.SubcategoryName = SubcategoryName;
        this.SubcategoryNameMs = SubcategoryNameMs;
        this.SubcategoryNameZh = SubcategoryNameZh;
        ListingId = new Collection<string>();
    }
    public Subcategory(string subcategoryId, string subcategoryName, string subcategoryNameMs, string subcategoryNameZh)
    {
        this.SubcategoryId = subcategoryId;
        this.SubcategoryName = subcategoryName;
        this.SubcategoryNameMs = subcategoryNameMs;
        this.SubcategoryNameZh = subcategoryNameZh;

    }
    public string SubcategoryId { get; set; }
    public string SubcategoryName { get; set; }
    public string SubcategoryNameMs { get; set; }
    public string SubcategoryNameZh { get; set; }
    public ICollection<string> ListingId { get; set; }

}
