using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Api.UseCases.Merchants;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Infrastructure.Data.MongoDb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantController : ControllerBase
    {
        private readonly ICreateMerchantUseCase _createMerchantUseCase;
        private readonly CreateMerchant.CreateMerchantPresenter _createMerchantPresenter;
        private readonly IMerchantRepository _merchantRepository;
 
        public MerchantController(ICreateMerchantUseCase createMerchantUseCase, CreateMerchant.CreateMerchantPresenter createMerchantPresenter, IMerchantRepository merchantRepository)
        { 
            _createMerchantUseCase = createMerchantUseCase;
            _createMerchantPresenter = createMerchantPresenter;
            _merchantRepository = merchantRepository;
        }
        
        // POST api/merchants
        [HttpPost]
        public ActionResult Post([FromBody] MerchantModel request)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            _createMerchantUseCase.Handle(new CreateMerchantRequest(request.CompanyName, request.CompanyRegistrationNumber, request.FirstName,request.LastName, request.Address, request.Phone, request.Fax),_createMerchantPresenter);
            return _createMerchantPresenter.ContentResult;
        }
    }
}