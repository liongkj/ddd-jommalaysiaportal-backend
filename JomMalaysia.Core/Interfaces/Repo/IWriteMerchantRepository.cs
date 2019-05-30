using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JomMalaysia.Core.Interfaces.Repo
{
    public interface IWriteMerchantRepository
    {
        Task Add();
        Task Update();
        Task Delete();
    }
}
