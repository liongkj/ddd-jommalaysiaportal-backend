
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Update
{
    public class UpdateMerchantRequest : IUseCaseRequest<UpdateMerchantResponse>
    {
        public UpdateMerchantRequest(string merchantId, Merchant Updated)
        {
            MerchantId = merchantId;
            this.Updated = Updated;
        }
        public string MerchantId { get; }
        public Merchant Updated { get; }


    }
}