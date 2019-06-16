using System;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Api.UseCases.Merchants.CreateMerchant;
using JomMalaysia.Api.UseCases.Merchants.DeleteMerchant;
using JomMalaysia.Api.UseCases.Merchants.GetMerchant;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.UseCases;
using JomMalaysia.Core.Services.UseCaseRequests;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Merchants
{
    [Route("api/[controller]")]
    [ApiController]
    public class MerchantsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICreateMerchantUseCase _createMerchantUseCase;
        private readonly CreateMerchantPresenter _createMerchantPresenter;
        private readonly IGetAllMerchantUseCase _getAllMerchantUseCase;
        private readonly GetAllMerchantPresenter _getAllMerchantPresenter;
        private readonly IGetMerchantUseCase _getMerchantUseCase;
        private readonly GetMerchantPresenter _getMerchantPresenter;
        private readonly IDeleteMerchantUseCase _deleteMerchantUseCase;
        private readonly DeleteMerchantPresenter _deleteMerchantPresenter;
        


        public MerchantsController(IMapper mapper,ICreateMerchantUseCase createMerchantUseCase,CreateMerchantPresenter MerchantPresenter, 
            IGetAllMerchantUseCase getAllMerchantUseCase,GetAllMerchantPresenter getAllMerchantPresenter, IGetMerchantUseCase getMerchantUseCase,GetMerchantPresenter getMerchantPresenter,
            IDeleteMerchantUseCase deleteMerchantUseCase,DeleteMerchantPresenter deleteMerchantPresenter)
        {
            _mapper = mapper;
            _createMerchantUseCase = createMerchantUseCase;
            _createMerchantPresenter = MerchantPresenter;
            _getAllMerchantUseCase = getAllMerchantUseCase;
            _getAllMerchantPresenter = getAllMerchantPresenter;
            _getMerchantUseCase = getMerchantUseCase;
            _getMerchantPresenter = getMerchantPresenter;
            _deleteMerchantUseCase = deleteMerchantUseCase;
            _deleteMerchantPresenter = deleteMerchantPresenter;
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
        public IActionResult Post([FromBody] MerchantDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Merchant m = _mapper.Map<MerchantDto, Merchant>(request);
            var req = new CreateMerchantRequest(m.CompanyName, m.CompanyRegistrationNumber, m.Contacts, m.Address);

            _createMerchantUseCase.Handle(req, _createMerchantPresenter);
            return _createMerchantPresenter.ContentResult;
        }

        //DELETE api/merchants/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var req = new DeleteMerchantRequest(id);
            _deleteMerchantUseCase.Handle(req, _deleteMerchantPresenter);
            return _deleteMerchantPresenter.ContentResult;

        }

        //PUT api/merchants/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, Merchant updatedMerchant)
        {
            throw new NotImplementedException();
        }


    }

}

