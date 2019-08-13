using AutoMapper;
using JomMalaysia.Api.Serialization;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.Enums;
using JomMalaysia.Core.Domain.Factories;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

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



        //// POST api/listings
        //[HttpPost("{MerchantId}")]
        //public IActionResult Create([FromRoute] string MerchantId, [FromBody] JObject ListingObject)
        //{

        //    var listing = JsonConvert.DeserializeObject<ListingHolder>(ListingObject.ToString(), new ListingConverter()).ConvertedListing;
        //    var list = _mapper.Map<Listing>(listing);
        //    //var validator = new CreateListingRequestValidator();
        //    var req = new CreateListingRequest(MerchantId, list);
        //    //var results = validator.Validate(req);

        //    _createListingUseCase.Handle(req, _listingPresenter);

        //    return _listingPresenter.ContentResult;
        //}

        // POST api/listings
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateListingRequest ListingObject)
        {

            //var results = validator.Validate(req);

            await _createListingUseCase.Handle(ListingObject, _listingPresenter);

            return _listingPresenter.ContentResult;
        }
    }
}