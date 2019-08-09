using AutoMapper;
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

        private readonly IGetAllListingUseCase _getAllListingUseCase;
        private readonly ListingPresenter _listingPresenter;
       
        //private readonly GetAllListingPresenter _getAllListingPresenter;
        //private readonly IGetListingUseCase _getListingUseCase;
        //private readonly GetListingPresenter _getListingPresenter;
        //private readonly IDeleteListingUseCase _deleteListingUseCase;
        //private readonly DeleteListingPresenter _deleteListingPresenter;
        //private readonly IUpdateListingUseCase _updateListingUseCase;
        //private readonly UpdateListingPresenter _updateListingPresenter;


        public ListingsController(IMapper mapper, ICreateListingUseCase createListingUseCase,
            
            ListingPresenter ListingPresenter
            //IGetAllListingUseCase getAllListingUseCase, IGetListingUseCase getListingUseCase, 
            //IDeleteListingUseCase deleteListingUseCase, 
            //IUpdateListingUseCase updateListingUseCase, 
            )
        {
            _mapper = mapper;
            _createListingUseCase = createListingUseCase;
            _listingPresenter = ListingPresenter;
            
            //_getAllListingUseCase = getAllListingUseCase;

            //_getListingUseCase = getListingUseCase;

            //_deleteListingUseCase = deleteListingUseCase;

            //_updateListingUseCase = updateListingUseCase;


        }
        /// <summary>
        /// Get list of listings
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Array of listing</returns>
        //GET api/listings

        ///Get details of listing
        //GET api/listings/{id}


        ///edit a listing
        //PUT api/listings/{id}

       

        // POST api/listings
        [HttpPost]
        public IActionResult Post([FromBody] Listing request,string merchantId)
        {
            var req = new CreateListingRequest(merchantId, request);
            _createListingUseCase.Handle(req,_listingPresenter);
            //new CreateListingRequest(request.MerchantId,l);

            //_createListingUseCase.Handle(req, _createListingPresenter);
            return _listingPresenter.ContentResult;
        }
    }
}