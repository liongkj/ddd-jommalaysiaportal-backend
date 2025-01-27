﻿using System.Collections.Generic;
using System.Threading.Tasks;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;
using JomMalaysia.Core.UseCases.MerchantUseCase.Delete;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using JomMalaysia.Core.UseCases.MerchantUseCase.Update;
using MongoDB.Driver;

namespace JomMalaysia.Core.Interfaces
{
    public interface IMerchantRepository
    {
        Task<CreateMerchantResponse> CreateMerchantAsync(Merchant merchant);
        Task<GetAllMerchantResponse> GetAllMerchantAsync();
        Task<DeleteMerchantResponse> DeleteMerchantAsync(string merchantId);
        GetMerchantResponse FindByName(string name);
        Task<GetMerchantResponse> FindByIdAsync(string merchantId);
        Task FindBySsmIdAsync(string ssmId);
        Task<UpdateMerchantResponse> UpdateMerchantAsyncWithSession(string id, Merchant updatedMerchant, IClientSessionHandle session = null);

    }
}
