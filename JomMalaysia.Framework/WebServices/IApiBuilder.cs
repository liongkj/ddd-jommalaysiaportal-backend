using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Framework.WebServices
{
    public interface IApiBuilder
    {
        string WebApiUrl { get; }
        string GetApi(string path, params string[] parameters);
    }
}
