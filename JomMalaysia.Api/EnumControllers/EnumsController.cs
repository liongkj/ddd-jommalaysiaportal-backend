using System;
using System.Threading.Tasks;
using AutoMapper;

using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.EnumControllers
{
    // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EnumsController : ControllerBase
    {
        #region dependencies
        private readonly IMapper _mapper;

        private readonly EnumsPresenter _categoryPresenter;



        public EnumsController(


            )
        {


        }
        #endregion


        //GET api/enums/{Enum}
        //GET api/enums/ListingType
        [HttpGet]
        public async Task<IActionResult> Get(int pageSize = 20, int pageNumber = 0)
        {
            throw new NotImplementedException();
        }

        //Get api/categories/{slug}



        // POST api/Categories
        [HttpPost]
        public async Task<IActionResult> Create()
        {
            throw new NotImplementedException();
        }

        //DELETE api/categories/{slug}
        [HttpDelete("{slug}")]
        public async Task<IActionResult> Delete(string slug)
        {
            throw new NotImplementedException();
        }
    }
}