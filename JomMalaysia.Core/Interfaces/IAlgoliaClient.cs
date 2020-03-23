using Algolia.Search.Clients;

namespace JomMalaysia.Core.Interfaces
{
    public interface IAlgoliaClient
    {
        SearchIndex Index { get; }
    }
}