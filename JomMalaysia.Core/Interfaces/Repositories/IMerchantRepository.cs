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
        GetAllMerchantResponse GetAllMerchants();
        DeleteMerchantResponse DeleteMerchant(string merchantId);
        GetMerchantResponse FindByName(string name);
        GetMerchantResponse FindById(string merchantId);
        UpdateMerchantResponse UpdateMerchant(string id, Merchant updatedMerchant);
        Task<UpdateMerchantResponse> UpdateMerchant(string id, Merchant updatedMerchant, IClientSessionHandle session);

    }
}
