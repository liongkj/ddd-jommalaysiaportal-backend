

using System.Threading.Tasks;
using JomMalaysia.Core.Dto;

namespace JomMalaysia.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<Token> GenerateEncodedToken(string id, string userName);
    }
}
