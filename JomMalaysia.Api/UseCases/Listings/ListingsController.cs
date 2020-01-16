using AutoMapper;
using JomMalaysia.Core.MobileUseCases.GetNearbyListings;
using JomMalaysia.Core.MobileUseCases.QueryListings;
using JomMalaysia.Core.MobileUseCases.SearchListings;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JomMalaysia.Api.UseCases.Listings
{
    [Route("api/[controller]")]
    [ApiController]
    // [Authorize(Policies.EDITOR)]
    public class ListingsController : ControllerBase
    {
        #region Dependencies
        private readonly ICreateListingUseCase _createListingUseCase;
        private readonly IGetAllListingUseCase _getAllListingUseCase;
        private readonly ListingPresenter _listingPresenter;
        private readonly IGetListingUseCase _getListingUseCase;
        private readonly IDeleteListingUseCase _deleteListingUseCase;

        private readonly IMapper _mapper;
        private readonly IUpdateListingUseCase _updateListingUseCase;

        private readonly IGetNearbyListingUseCase _getNearbyListingUseCase;

        private readonly IQueryListingUseCase _queryListingUseCase;
        private readonly ISearchListingUseCase _searchListingUseCase;

        public ListingsController(ICreateListingUseCase createListingUseCase,

            ListingPresenter ListingPresenter,
            IGetAllListingUseCase getAllListingUseCase,
            IGetListingUseCase getListingUseCase,
            IDeleteListingUseCase deleteListingUseCase,
             IUpdateListingUseCase updateListingUseCase,
            ISearchListingUseCase searchListingUseCase,
        IGetNearbyListingUseCase getNearbyListingUseCase,
        IQueryListingUseCase queryListingUseCase


            )
        {
            _createListingUseCase = createListingUseCase;
            _listingPresenter = ListingPresenter;
            _getAllListingUseCase = getAllListingUseCase;
            _getListingUseCase = getListingUseCase;
            _searchListingUseCase = searchListingUseCase;
            _deleteListingUseCase = deleteListingUseCase;

            _updateListingUseCase = updateListingUseCase;

            _getNearbyListingUseCase = getNearbyListingUseCase;
            _queryListingUseCase = queryListingUseCase;

        }
        #endregion

        #region portal
        /// <summary>
        /// Get list of listings
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Array of listing</returns>
        //GET api/listings
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //TODO add query and paging
            await _getAllListingUseCase.Handle(new GetAllListingRequest(), _listingPresenter);
            return _listingPresenter.ContentResult;
        }



        ///Get details of listing
        //GET api/listings/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetListingRequest req)
        {

            await _getListingUseCase.Handle(req, _listingPresenter);

            return _listingPresenter.ContentResult;
        }

        // POST api/listings
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CoreListingRequest ListingObject)
        {
            await _createListingUseCase.Handle(ListingObject, _listingPresenter);

            return _listingPresenter.ContentResult;
        }
        ///delete listing
        ///DELETE api/listings/{id}
        ///
        [HttpDelete("{ListingId}")]
        public async Task<IActionResult> Delete([FromRoute] string ListingId)
        {
            var req = new DeleteListingRequest(ListingId);
            await _deleteListingUseCase.Handle(req, _listingPresenter);
            return _listingPresenter.ContentResult;
        }

        ///edit a listing
        //PUT api/listings/{id}

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, [FromBody] CoreListingRequest ListingObject)
        {
            ListingObject.ListingId = id;
            await _updateListingUseCase.Handle(ListingObject, _listingPresenter);
            return _listingPresenter.ContentResult;
        }


        #endregion

        [HttpGet("query")]
        public async Task<IActionResult> GetListingsOfCategory([FromQuery] QueryListingRequest req)
        {
            //TODO add query and paging
            await _queryListingUseCase.Handle(req, _listingPresenter);
            return _listingPresenter.ContentResult;
        }

        // location=-33.8670522,151.1957362&radius=1500&type=restaurant&keyword=cruise
        [HttpGet("nearby")]
        public async Task<IActionResult> GetListingsWithinRadius([FromQuery] GetNearbyListingRequest req)
        {
            await _getNearbyListingUseCase.Handle(req, _listingPresenter);
            return _listingPresenter.ContentResult;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchListing([FromQuery] SearchListingRequest req)
        {
            await _searchListingUseCase.Handle(req, _listingPresenter);
            return _listingPresenter.ContentResult;
        }
    }
}