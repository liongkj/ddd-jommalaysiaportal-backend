using System;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

using JomMalaysia.Core.Interfaces.UseCases.Merchants;
using JomMalaysia.Core.Services.Merchants.UseCaseRequests;
using JomMalaysia.Core.Services.Merchants.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.MerchantUseCase
{
    public class DeleteMerchantUseCase : IDeleteMerchantUseCase
    {
        private readonly IMerchantRepository _merchant;
        public DeleteMerchantUseCase(IMerchantRepository merchant)
        {
            _merchant = merchant;
        }

        public bool Handle(DeleteMerchantRequest message, IOutputPort<DeleteMerchantResponse> outputPort)
        {
            if (message.MerchantId == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            Merchant merchant = (_merchant.FindById(message.MerchantId)).Merchant;
            if (merchant == null)
            {
                outputPort.Handle(new DeleteMerchantResponse(message.MerchantId,false,"Merchant Not Found"));
                return false;
            }
            else
            {
                if (merchant.Listings.Count > 0)
                {
                    //still have listing, cannot delete
                    outputPort.Handle(new DeleteMerchantResponse(message.MerchantId, false, "Merchant still has listing associated"));
                }
                var response = _merchant.Delete(message.MerchantId);
                outputPort.Handle(new DeleteMerchantResponse(message.MerchantId, true , merchant.MerchantId+" deleted"));
                return response.Success;
            }
        }
    }
}