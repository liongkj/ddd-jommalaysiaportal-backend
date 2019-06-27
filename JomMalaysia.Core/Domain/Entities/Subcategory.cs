using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;

public class Subcategory
{
    internal Category Category { get; private set; }

    internal Subcategory(Category category, string SubcategoryName, string SubcategoryNameMs, string SubcategoryNameZh)
    {
        this.Category = category;
        this.SubcategoryName = SubcategoryName;
        this.SubcategoryNameMs = SubcategoryNameMs;
        this.SubcategoryNameZh = SubcategoryNameZh;
        ListingIds = new Collection<string>();
    }

    public void AddListingId(string ListingId)
    {
        ListingIds.Add(ListingId);
    }

    public void RemoveListingId(string ListingId)
    {
        ListingIds.Remove(ListingId);
    }
    public string SubcategoryId { get; set; }
    public string SubcategoryName { get; set; }
    public string SubcategoryNameMs { get; set; }
    public string SubcategoryNameZh { get; set; }
    public ICollection<string> ListingIds { get; set; }

}
