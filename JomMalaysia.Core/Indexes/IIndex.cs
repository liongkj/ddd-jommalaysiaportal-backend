using System.Collections.Generic;
using System.Threading.Tasks;
using Algolia.Search.Models.Common;

namespace JomMalaysia.Core.Indexes
{
    public interface IIndex<T>
    {
        Task<BatchIndexingResponse> SaveObject(IEnumerable<T> items);
    }
}