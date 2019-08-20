using AutoMapper;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace JomMalaysia.Api.UseCases.Listings
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListingsController : ControllerBase
    {
        private readonly ICreateListingUseCase _createListingUseCase;
        private readonly IGetAllListingUseCase _getAllListingUseCase;
        private readonly ListingPresenter _listingPresenter;
        private readonly IGetListingUseCase _getListingUseCase;
        private readonly IDeleteListingUseCase _deleteListingUseCase;
        //private readonly IUpdateListingUseCase _updateListingUseCase;
        


        public ListingsController(ICreateListingUseCase createListingUseCase,

            ListingPresenter ListingPresenter,
            IGetAllListingUseCase getAllListingUseCase,
            IGetListingUseCase getListingUseCase,
            IDeleteListingUseCase deleteListingUseCase
            //IUpdateListingUseCase updateListingUseCase, 
            )
        {
            _createListingUseCase = createListingUseCase;
            _listingPresenter = ListingPresenter;
            _getAllListingUseCase = getAllListingUseCase;
            _getListingUseCase = getListingUseCase;

            _deleteListingUseCase = deleteListingUseCase;

            //_updateListingUseCase = updateListingUseCase;


        }
        /// <summary>
        /// Get list of listings
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Array of listing</returns>
        //GET api/listings
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _getAllListingUseCase.Handle(new GetAllListingRequest(), _listingPresenter);
            return _listingPresenter.ContentResult;
        }

        ///Get details of listing
        //GET api/listings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {

            await _getListingUseCase.Handle(new GetListingRequest(id), _listingPresenter);

            return _listingPresenter.ContentResult;
        }

        ///delete listing
        ///DELETE api/listings/{id}
        ///
        [HttpDelete("{ListingId}")]
        public async Task<IActionResult> Delete([FromRoute]string ListingId)
        {
            await _deleteListingUseCase.Handle(new DeleteListingRequest(ListingId), _listingPresenter);
            return _listingPresenter.ContentResult;
        }

        ///edit a listing
        //PUT api/listings/{id}
        //TODO update listing api

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