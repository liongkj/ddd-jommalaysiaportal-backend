using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Api.UseCases.Merchants.CreateMerchant;
using JomMalaysia.Api.UseCases.Merchants.DeleteMerchant;
using JomMalaysia.Api.UseCases.Merchants.GetMerchant;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.MerchantUseCase.Create;
using JomMalaysia.Core.UseCases.MerchantUseCase.Delete;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get;
using JomMalaysia.Core.UseCases.MerchantUseCase.Get.Request;
using JomMalaysia.Core.UseCases.MerchantUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
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
        private readonly IUpdateMerchantUseCase _updateMerchantUseCase;
        private readonly UpdateMerchantPresenter _updateMerchantPresenter;

        public MerchantsController(IMapper mapper,
            ICreateMerchantUseCase createMerchantUseCase, CreateMerchantPresenter MerchantPresenter,
            IGetAllMerchantUseCase getAllMerchantUseCase, GetAllMerchantPresenter getAllMerchantPresenter,
            IGetMerchantUseCase getMerchantUseCase, GetMerchantPresenter getMerchantPresenter,
            IDeleteMerchantUseCase deleteMerchantUseCase, DeleteMerchantPresenter deleteMerchantPresenter,
            IUpdateMerchantUseCase updateMerchantUseCase, UpdateMerchantPresenter updateMerchantPresenter)
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
            _updateMerchantUseCase = updateMerchantUseCase;
            _updateMerchantPresenter = updateMerchantPresenter;

        }

        //GET api/merchants
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _getAllMerchantUseCase.Handle(new GetAllMerchantRequest(), _getAllMerchantPresenter).ConfigureAwait(false);

            return _getAllMerchantPresenter.ContentResult;
        }

        //GET api/merchants/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            if (id == null) return StatusCode((int)HttpStatusCode.BadRequest);
            var req = new GetMerchantRequest(id);
            await _getMerchantUseCase.Handle(req, _getMerchantPresenter).ConfigureAwait(false);
            return _getMerchantPresenter.ContentResult;
        }

        // POST api/merchants
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateMerchantRequest request)
        {
            await _createMerchantUseCase.Handle(request, _createMerchantPresenter);
            return _createMerchantPresenter.ContentResult;
        }

        //DELETE api/merchants/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var req = new DeleteMerchantRequest(id);
            await _deleteMerchantUseCase.Handle(req, _deleteMerchantPresenter);
            return _deleteMerchantPresenter.ContentResult;

        }


        //PUT api/merchants/{id}
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] UpdateMerchantRequest updatedMerchant)
        {

            _updateMerchantUseCase.Handle(updatedMerchant, _updateMerchantPresenter);
            return _updateMerchantPresenter.ContentResult;
        }

        //Get api/merchants/{id}/listings

    }

}
