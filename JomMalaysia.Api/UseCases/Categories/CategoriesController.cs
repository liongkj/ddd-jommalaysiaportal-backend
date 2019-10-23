using System;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Domain.ValueObjects;
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
            IGetCategoryByNameUseCase getCategoryByNameUseCase,
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

        //Get api/categories/{slug}
        [HttpGet("{slug}")]
        public async Task<IActionResult> Get(string slug)
        {
            var req = new GetCategoryByIdRequest(slug);
            try { await _getCategoryByIdUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false); }
            catch (Exception e) { throw e; }
            return _categoryPresenter.ContentResult;
        }


        // POST api/Categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto request)
        {
            Category cat = _mapper.Map<CategoryDto, Category>(request);
            Image img = new Image(request.CategoryThumbnail.Url, request.CategoryThumbnail.ThumbnailUrl);

            var req = new CreateCategoryRequest(cat.CategoryCode, cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh, img, request.ParentCategory);

            await _createCategoryUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false);
            return _categoryPresenter.ContentResult;
        }


        [HttpPost("{id}/subcategories")]
        public async Task<IActionResult> CreateSubcategory([FromRoute] string id, [FromBody] CategoryDto request)
        {
            Image img = new Image(request.CategoryThumbnail.Url, request.CategoryThumbnail.ThumbnailUrl);
            try
            {
                Category cat = _mapper.Map<CategoryDto, Category>(request);

                var req = new CreateCategoryRequest(cat.CategoryCode, cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh, img, id);


                await _createCategoryUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw e;
            }
            return _categoryPresenter.ContentResult;
        }


        //DELETE api/categories/{slug}
        [HttpDelete("{id}")]

        public async Task<IActionResult> Delete(string id)
        {
            var req = new DeleteCategoryRequest(id);
            await _deleteCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        //PUT api/categories/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(string id, [FromBody] CategoryDto Updated)
        {
            Category updated = _mapper.Map<Category>(Updated);
            var req = new UpdateCategoryRequest(id, updated.CategoryCode, updated.CategoryName, updated.CategoryNameMs, updated.CategoryNameZh, updated.CategoryThumbnail);
            await _updateCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

    }
}