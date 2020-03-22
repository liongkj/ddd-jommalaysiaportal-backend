using Algolia.Search.Clients;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Infrastructure.Algolia
{
    public class AlgoliaClient:IAlgoliaClient
    {
        public SearchClient Client { get; }
        public SearchIndex Index { get; }

         public AlgoliaClient(IAlgoliaSettings settings)
        {
            Client= new SearchClient(settings.AppId, settings.ApiKey);
            Index = Client.InitIndex(settings.IndexName);
        }
        
    }
}