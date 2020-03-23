using System;
using Algolia.Search.Clients;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Infrastructure.Algolia
{
    public class AlgoliaClient : IAlgoliaClient
    {
        private SearchClient _client;
        public SearchIndex Index { get; set; }

        public AlgoliaClient(IAlgoliaSetting config)
        {
            try
            {
                _client = new SearchClient(config.AppId, config.ApiKey);
                Index = _client.InitIndex(config.IndexName);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}