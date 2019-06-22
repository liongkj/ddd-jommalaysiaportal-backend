using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.Services.Categories.UseCaseRequests
{
    public class DeleteSubcategoryRequest : IUseCaseRequest<DeleteSubcategoryResponse>
    {
        public string SubcategoryId { get; set; }
        public ICollection<string> ListingId { get; private set; }
        public string CategoryId { get; private set; }

        public DeleteSubcategoryRequest(string SubcategoryId)
        {
            if (string.IsNullOrWhiteSpace(CategoryId))
            {
                throw new System.ArgumentException("Delete Category: Subcategory Id null", nameof(CategoryId));
            }
            ListingId = new Collection<string>();

            this.SubcategoryId = SubcategoryId;
        }
    }
}