using System.Collections.Generic;
using System.Threading.Tasks;
using Algolia.Search.Models.Common;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;

namespace JomMalaysia.Core.Indexes
{
    public interface IListingIndexer
    {
        Task<BatchIndexingResponse> SaveObject(IEnumerable<ListingViewModel> items);
    }
}