using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Services.UseCaseResponses;

namespace JomMalaysia.Core.Interfaces
{
    public interface IMerchantRepository
    {
        CreateMerchantResponse CreateMerchant(Merchant merchant);
        Task<GetAllMerchantResponse> GetAllMerchants();
        DeleteMerchantResponse Delete(string id);
        Task<Merchant> FindByName(string name);
        Task<Merchant> FindById(string id);
        UpdateMerchantResponse Update(string id, Merchant merchant);
    }
}
