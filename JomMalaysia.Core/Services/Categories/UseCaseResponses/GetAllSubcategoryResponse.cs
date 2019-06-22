using System.Collections.Generic;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.Services.Categories.UseCaseResponses
{
    public class GetAllSubcategoryResponse : UseCaseResponseMessage
    {
        public List<Subcategory> Subcategories { get; }
        public IEnumerable<string> Errors { get; }

        public GetAllSubcategoryResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetAllSubcategoryResponse(List<Subcategory> Subcategories, bool success = false, string message = null) : base(success, message)
        {
            this.Subcategories = Subcategories;
        }
    }
}