using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JomMalaysia.Core.Interfaces
{
    public interface ITransaction : IDisposable
    {
        Task Complete();
    }
}