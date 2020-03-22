using Algolia.Search.Clients;

namespace JomMalaysia.Core.Interfaces
{
    public interface IAlgoliaClient
    {
        SearchClient Client { get;  }
        SearchIndex Index { get; }
    }
}