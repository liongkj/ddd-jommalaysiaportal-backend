using System;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.UseCases.CategoryUseCase.Create;
using JomMalaysia.Core.UseCases.CategoryUseCase.Update;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        #region dependencies
        private readonly IMapper _mapper;
        private readonly ICreateCategoryUseCase _createCategoryUseCase;
        private readonly IGetAllCategoryUseCase _getAllCategoryUseCase;
        private readonly IGetCategoryByIdUseCase _getCategoryByIdUseCase;
        private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;
        private readonly IUpdateCategoryUseCase _updateCategoryUseCase;
        private readonly CategoryPresenter _categoryPresenter;

        public CategoriesController(IMapper mapper,
        ICreateCategoryUseCase createCategoryUseCase,
            IGetAllCategoryUseCase getAllCategoryUseCase,
            IGetCategoryByIdUseCase getCategoryByIdUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase,
            IUpdateCategoryUseCase updateCategoryUseCase,
            CategoryPresenter categoryPresenter
        )
        {
            _mapper = mapper;
            _createCategoryUseCase = createCategoryUseCase;
            _getAllCategoryUseCase = getAllCategoryUseCase;
            _getCategoryByIdUseCase = getCategoryByIdUseCase;
            _deleteCategoryUseCase = deleteCategoryUseCase;
            _updateCategoryUseCase = updateCategoryUseCase;
            _getCategoryByIdUseCase = getCategoryByIdUseCase;
            _categoryPresenter = categoryPresenter;
        }
        #endregion
        [HttpGet]

        public async Task<IActionResult> Get(int pageSize = 20, int pageNumber = 0)
        {
            var req = new GetAllCategoryRequest
            {
                PageSize = pageSize,
                PageNumber = pageNumber
            };
            await _getAllCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        //Get api/categories/{id}
        [HttpGet("{CategoryId}")]
        public async Task<IActionResult> Get([FromRoute] string categoryId)
        {
            var req = new GetCategoryByIdRequest
            {
                CategoryId = categoryId,
            };
            try { await _getCategoryByIdUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false); }
            catch (Exception e) { throw e; }
            return _categoryPresenter.ContentResult;
        }


        // POST api/Categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            await _createCategoryUseCase.Handle(request, _categoryPresenter).ConfigureAwait(false);
            return _categoryPresenter.ContentResult;
        }


        [HttpPost("{id}/subcategories")]
        public async Task<IActionResult> CreateSubcategory([FromRoute] string id, [FromBody] CreateCategoryRequest request)
        {

            try
            {
                request.ParentCategory = id;
                await _createCategoryUseCase.Handle(request, _categoryPresenter).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw e;
            }
            return _categoryPresenter.ContentResult;
        }


        //DELETE api/categories/{id}
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete([FromRoute]DeleteCategoryRequest req)
        {
            await _deleteCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        //PUT api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] UpdateCategoryRequest req)
        {
            req.CategoryId = id;
            await _updateCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

    }
}