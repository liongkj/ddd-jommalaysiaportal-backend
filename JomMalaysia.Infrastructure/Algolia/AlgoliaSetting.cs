using Algolia.Search.Clients;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Infrastructure.Algolia
{
    public class AlgoliaSetting:IAlgoliaSetting
    {
        public string AppId { get; set; }
        public string ApiKey { get; set; }
        public string IndexName { get; set; }
    }
}