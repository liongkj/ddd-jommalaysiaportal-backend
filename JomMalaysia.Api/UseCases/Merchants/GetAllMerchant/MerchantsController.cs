using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseRequests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Merchants.GetAllMerchant
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IGetAllMerchantUseCase _getAllMerchantUseCase;
        private readonly GetAllMerchantPresenter _getAllMerchantPresenter;

        public MerchantsController(GetAllMerchantPresenter getAllMerchantPresenter, IGetAllMerchantUseCase getAllMerchantUseCase)
        {
            _getAllMerchantPresenter = getAllMerchantPresenter;
            _getAllMerchantUseCase = getAllMerchantUseCase;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _getAllMerchantUseCase.Handle(new GetAllMerchantRequest(),_getAllMerchantPresenter);
            
            return _getAllMerchantPresenter.ContentResult;
        }
    }
}