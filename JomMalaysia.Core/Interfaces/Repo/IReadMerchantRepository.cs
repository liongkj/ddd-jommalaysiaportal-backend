using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Entities;

namespace JomMalaysia.Core.Interfaces.Repo
{
    public interface IReadMerchantRepository
    {
        Task<Merchant> Get(Guid id);
    }
}
