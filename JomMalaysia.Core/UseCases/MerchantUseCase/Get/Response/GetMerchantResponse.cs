using System.Collections.Generic;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.MerchantUseCase.Get.Response
{
    public class GetMerchantResponse : UseCaseResponseMessage
    {
        public Merchant Merchant { get; }
        
        public MerchantViewModel Data { get; }
        public IEnumerable<string> Errors { get; }

        public GetMerchantResponse(IEnumerable<string> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public GetMerchantResponse(Merchant merchant, bool success = false, string message = null) : base(success, message)
        {
            Merchant = merchant;
        }
        public GetMerchantResponse(MerchantViewModel listing, bool success = true, string message = null) : base(success, message)
        {
            Data = listing;
        }
    }
}
