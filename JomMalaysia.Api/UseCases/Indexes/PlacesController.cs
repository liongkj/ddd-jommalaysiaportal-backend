using System;
using System.Threading.Tasks;
using Autofac.Features.Indexed;
using JomMalaysia.Api.UseCases.Listings;
using JomMalaysia.Core.Indexes;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.MobileUseCases.GetNearbyListings;
using JomMalaysia.Core.MobileUseCases.QueryListings;
using JomMalaysia.Core.MobileUseCases.SearchListings;
using JomMalaysia.Core.UseCases.ListingUseCase.Create;
using JomMalaysia.Core.UseCases.ListingUseCase.Delete;
using JomMalaysia.Core.UseCases.ListingUseCase.Get;
using JomMalaysia.Core.UseCases.ListingUseCase.Shared;
using JomMalaysia.Core.UseCases.ListingUseCase.Update;
using JomMalaysia.Infrastructure.Algolia;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Indexes
{
    [Route("api/algolia/[controller]")]
    [ApiController]

    // [Authorize(Policies.EDITOR)]
    public class PlacesController : ControllerBase
    {
        #region Dependencies

        
        private readonly IBatchIndexPlacesUseCase _batchIndexPlaces;
        private readonly IndexPresenter _presenter;

        public PlacesController( IBatchIndexPlacesUseCase batchIndexPlaces,IndexPresenter presenter)
        {
            
            _presenter = presenter;
            _batchIndexPlaces = batchIndexPlaces;
        }
        
        
        #endregion

        [HttpPost(nameof(Batch))]
        public async Task<IActionResult> Batch()
        {
            await _batchIndexPlaces.Handle(new AlgoliaIndexRequest(), _presenter);
            return _presenter.ContentResult;
        }

    }
}