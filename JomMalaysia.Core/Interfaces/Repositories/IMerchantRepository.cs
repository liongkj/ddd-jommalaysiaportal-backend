using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;
using JomMalaysia.Core.UseCases.MerchantUseCase.Delete;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using JomMalaysia.Core.UseCases.MerchantUseCase.Update;

namespace JomMalaysia.Core.Interfaces
{
    public interface IMerchantRepository
    {
        Task<CreateMerchantResponse> CreateMerchant(Merchant merchant);
        Task<GetAllMerchantResponse> GetAllMerchants();
        DeleteMerchantResponse DeleteMerchant(string merchantId);
        GetMerchantResponse FindByName(string name);
        GetMerchantResponse FindById(string merchantId);
        Task<UpdateMerchantResponse> UpdateMerchant(string id, Merchant updatedMerchant);

    }
}
