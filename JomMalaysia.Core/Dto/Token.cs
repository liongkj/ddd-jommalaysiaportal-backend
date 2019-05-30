using System;
using System.Collections.Generic;
using System.Text;

namespace JomMalaysia.Core.Dto
{
    public sealed class Token
    {
        public string Id { get; }
        public string AuthToken { get; }
        public int ExpiresIn { get; }

        public Token(string id, string authToken, int expiresIn)
        {
            Id = id;
            AuthToken = authToken;
            ExpiresIn = expiresIn;
        }
    }
}
