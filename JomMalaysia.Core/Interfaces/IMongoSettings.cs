
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces
{
    public interface IMongoSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }

    }
}
