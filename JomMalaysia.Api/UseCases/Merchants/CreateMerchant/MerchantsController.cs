using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Api.UseCases.Merchants;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using JomMalaysia.Infrastructure.Data.MongoDb.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.CreateMerchant
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly ICreateMerchantUseCase _createMerchantUseCase;
        private readonly CreateMerchant.CreateMerchantPresenter _createMerchantPresenter;
        private readonly IMerchantRepository _merchantRepository;


        public MerchantsController(ICreateMerchantUseCase createMerchantUseCase, CreateMerchantPresenter createMerchantPresenter, IMerchantRepository merchantRepository, IMapper mapper)
        {
            _createMerchantUseCase = createMerchantUseCase;
            _createMerchantPresenter = createMerchantPresenter;
            _merchantRepository = merchantRepository;

        }

        // POST api/merchants
        [HttpPost]
        public IActionResult Post([FromBody] MerchantViewModel request)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var req = new CreateMerchantRequest(request.CompanyName, request.CompanyRegistrationNumber, request.Name, request.Address, request.Phone, request.Fax, (Email)request.Email);

            _createMerchantUseCase.Handle(req, _createMerchantPresenter);
            return _createMerchantPresenter.ContentResult;
        }
    }
}