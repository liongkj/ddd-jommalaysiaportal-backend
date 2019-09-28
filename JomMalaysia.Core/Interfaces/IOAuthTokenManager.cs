using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JomMalaysia.Core.Interfaces
{
    public interface IOAuthTokenManager
    {
        Task<string> GetAccessToken(string type = "null");
    }
}
