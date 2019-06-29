using AutoMapper;
using JomMalaysia.Api.UseCases.Listings.CreateListing;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Listings
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICreateListingUseCase _createListingUseCase;
        private readonly CreateListingPresenter _createListingPresenter;
        private readonly IGetAllListingUseCase _getAllListingUseCase;
        //private readonly GetAllListingPresenter _getAllListingPresenter;
        //private readonly IGetListingUseCase _getListingUseCase;
        //private readonly GetListingPresenter _getListingPresenter;
        //private readonly IDeleteListingUseCase _deleteListingUseCase;
        //private readonly DeleteListingPresenter _deleteListingPresenter;
        //private readonly IUpdateListingUseCase _updateListingUseCase;
        //private readonly UpdateListingPresenter _updateListingPresenter;


        public ListingsController(IMapper mapper, ICreateListingUseCase createListingUseCase, CreateListingPresenter ListingPresenter
            //IGetAllListingUseCase getAllListingUseCase, GetAllListingPresenter getAllListingPresenter, IGetListingUseCase getListingUseCase, GetListingPresenter getListingPresenter,
            //IDeleteListingUseCase deleteListingUseCase, DeleteListingPresenter deleteListingPresenter,
            //IUpdateListingUseCase updateListingUseCase, UpdateListingPresenter updateListingPresenter
            )
        {
            _mapper = mapper;
            _createListingUseCase = createListingUseCase;
            _createListingPresenter = ListingPresenter;
            //_getAllListingUseCase = getAllListingUseCase;
            //_getAllListingPresenter = getAllListingPresenter;
            //_getListingUseCase = getListingUseCase;
            //_getListingPresenter = getListingPresenter;
            //_deleteListingUseCase = deleteListingUseCase;
            //_deleteListingPresenter = deleteListingPresenter;
            //_updateListingUseCase = updateListingUseCase;
            //_updateListingPresenter = updateListingPresenter;

        }

        // POST api/listings
        [HttpPost]
        public IActionResult Post([FromBody] ListingDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Listing l = _mapper.Map<ListingDto, Listing>(request);

            var req = l;
            //new CreateListingRequest(request.MerchantId,l);

            //_createListingUseCase.Handle(req, _createListingPresenter);
            return _createListingPresenter.ContentResult;
        }
    }
}