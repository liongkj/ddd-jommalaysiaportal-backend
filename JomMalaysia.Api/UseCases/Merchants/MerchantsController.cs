using System;
using System.Threading.Tasks;
using JomMalaysia.Api.UseCases.Merchants.CreateMerchant;
using JomMalaysia.Api.UseCases.Merchants.DeleteMerchant;
using JomMalaysia.Api.UseCases.Merchants.GetMerchant;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases;
using JomMalaysia.Core.Services.UseCaseRequests;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Merchants
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly ICreateMerchantUseCase _createMerchantUseCase;
        private readonly CreateMerchantPresenter _createMerchantPresenter;
        private readonly IGetAllMerchantUseCase _getAllMerchantUseCase;
        private readonly GetAllMerchantPresenter _getAllMerchantPresenter;
        private readonly IGetMerchantUseCase _getMerchantUseCase;
        private readonly GetMerchantPresenter _getMerchantPresenter;
        private readonly IDeleteMerchantUseCase _deleteMerchantUseCase;
        private readonly DeleteMerchantPresenter _deleteMerchantPresenter;
        


        public MerchantsController(ICreateMerchantUseCase createMerchantUseCase, CreateMerchantPresenter MerchantPresenter, IGetAllMerchantUseCase getAllMerchantUseCase, GetAllMerchantPresenter getAllMerchantPresenter, IGetMerchantUseCase getMerchantUseCase, GetMerchantPresenter getMerchantPresenter)
        {
            _createMerchantUseCase = createMerchantUseCase;
            _createMerchantPresenter = MerchantPresenter;
            _getAllMerchantUseCase = getAllMerchantUseCase;
            _getAllMerchantPresenter = getAllMerchantPresenter;
            _getMerchantUseCase = getMerchantUseCase;
            _getMerchantPresenter = getMerchantPresenter;
        }

        //GET api/merchants
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _getAllMerchantUseCase.Handle(new GetAllMerchantRequest(), _getAllMerchantPresenter);

            return _getAllMerchantPresenter.ContentResult;
        }

        //GET api/merchants/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var req = new GetMerchantRequest(id);
            _getMerchantUseCase.Handle(req, _getMerchantPresenter);
            return _getMerchantPresenter.ContentResult;
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

        //DELETE api/merchant/id
        [HttpDelete]
        public IActionResult Delete(string id)
        {
            throw new NotImplementedException();
        }

    }

}

