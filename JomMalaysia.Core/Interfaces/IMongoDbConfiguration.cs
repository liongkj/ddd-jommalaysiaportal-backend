
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces
{
    public interface IMongoDbConfiguration
    {
        IMongoDatabase Database { get;  }
        IClientSessionHandle Session { get; }
        Task<IClientSessionHandle> StartSession(CancellationToken cancellactionToken = default);
    }
}
