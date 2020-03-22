using Algolia.Search.Clients;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Infrastructure.Algolia
{
    public class AlgoliaSettings:IAlgoliaSettings
    {
        public string AppId { get; }
        public string ApiKey { get; }
        public string IndexName { get; }
    }
}