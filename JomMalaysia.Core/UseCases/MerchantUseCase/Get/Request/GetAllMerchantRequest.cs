using System.Collections.Generic;
using System.Collections.ObjectModel;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request
{
    public class GetAllMerchantRequest:IUseCaseRequest<GetAllMerchantResponse>
    {
       public ICollection<Merchant> Merchants { get; set; }
        public string Id { get; set; }
        public GetAllMerchantRequest()
        {
            Merchants = new Collection<Merchant>();
        }
    }
}
