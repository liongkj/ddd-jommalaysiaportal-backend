using System.Collections.Generic;
using System.Threading.Tasks;
using Algolia.Search.Models.Common;
using JomMalaysia.Core.Indexes;

namespace JomMalaysia.Infrastructure.Algolia
{
    public interface IIndex<T>
    {
        Task<BatchIndexingResponse> SaveObject(IEnumerable<T> items);
    }
}