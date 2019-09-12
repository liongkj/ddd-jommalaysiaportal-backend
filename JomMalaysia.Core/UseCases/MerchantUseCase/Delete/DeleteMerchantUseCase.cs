using System;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Domain.Entities;
using System.Threading.Tasks;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;
using System.Collections.Generic;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Delete
{
    public class DeleteMerchantUseCase : IDeleteMerchantUseCase
    {
        private readonly IMerchantRepository _merchant;
        public DeleteMerchantUseCase(IMerchantRepository merchant)
        {
            _merchant = merchant;
        }

        public async Task<bool> Handle(DeleteMerchantRequest message, IOutputPort<DeleteMerchantResponse> outputPort)
        {

            GetMerchantResponse query;
            DeleteMerchantResponse response;
            try
            {
                query = await _merchant.FindByIdAsync(message.MerchantId).ConfigureAwait(false);

                if (query.Success)
                {//found
                    if (!query.Merchant.IsSafeToDelete())
                    {//still have listing, cannot delete
                        outputPort.Handle(new DeleteMerchantResponse(message.MerchantId, false, "Merchant still has listing associated"));
                        return false;
                    }
                    else
                    {
                        response = await _merchant.DeleteMerchantAsync(message.MerchantId).ConfigureAwait(false);
                        outputPort.Handle(response);
                        return response.Success;
                    }
                }
                else
                {
                    //merchant not found
                    outputPort.Handle(new DeleteMerchantResponse(query.Errors, false, "Merchant Not Found"));
                    return false;
                }
            }
            catch (Exception e)
            {
                outputPort.Handle(new DeleteMerchantResponse(new List<string> { e.ToString() }));
                return false;
            }
        }
    }
}
