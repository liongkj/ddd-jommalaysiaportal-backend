
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces
{
    public interface IMongoDbContext
    {
        IMongoDatabase Database { get;  }
        IClientSessionHandle Session { get; }
        Task<IClientSessionHandle> StartSession(CancellationToken cancellactionToken = default);
    }
}
