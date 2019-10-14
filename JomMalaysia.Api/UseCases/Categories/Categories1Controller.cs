using System;
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Api.Scope;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Delete;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Update;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Categories
{
    // [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    // [Authorize(Policies.EDITOR)]
    public class Categories1Controller : ControllerBase
    {
        #region dependencies
        private readonly IMapper _mapper;
        private readonly ICreateCategoryUseCase _createCategoryUseCase;
        private readonly IGetAllCategoryUseCase _getAllCategoryUseCase;
        private readonly IGetCategoryByIdUseCase _getCategoryByIdUseCase;
        private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;
        private readonly IUpdateCategoryUseCase _updateCategoryUseCase;
        private readonly IGetCategoryByNameUseCase _getCategoryByNameUseCase;
        private readonly CategoryPresenter _categoryPresenter;

        private readonly IGetAllSubcategoryUseCase _getAllSubcategoryUseCase;
        private readonly IDeleteSubcategoryUseCase _deleteSubcategoryUseCase;
        private readonly IUpdateSubcategoryUseCase _updateSubcategoryUseCase;

        public Categories1Controller(IMapper mapper,
        ICreateCategoryUseCase createCategoryUseCase,
            IGetAllCategoryUseCase getAllCategoryUseCase,
            IGetCategoryByIdUseCase getCategoryByIdUseCase,
            IGetCategoryByNameUseCase getCategoryByNameUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase,
            IUpdateCategoryUseCase updateCategoryUseCase,
            CategoryPresenter categoryPresenter,
            IGetAllSubcategoryUseCase getAllSubcategoryUseCase,
            IDeleteSubcategoryUseCase deleteSubcategoryUseCase,
            IUpdateSubcategoryUseCase updateSubcategoryUseCase
            )
        {
            _mapper = mapper;
            _createCategoryUseCase = createCategoryUseCase;
            _getAllCategoryUseCase = getAllCategoryUseCase;
            _getCategoryByIdUseCase = getCategoryByIdUseCase;
            _deleteCategoryUseCase = deleteCategoryUseCase;
            _updateCategoryUseCase = updateCategoryUseCase;
            _getCategoryByNameUseCase = getCategoryByNameUseCase;
            _categoryPresenter = categoryPresenter;
            _getAllSubcategoryUseCase = getAllSubcategoryUseCase;
            _deleteSubcategoryUseCase = deleteSubcategoryUseCase;
            _updateSubcategoryUseCase = updateSubcategoryUseCase;
        }
        #endregion
        #region category

        //GET api/categories
        //get whole category collection
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
            var req = new GetCategoryByNameRequest(slug);
            try { await _getCategoryByNameUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false); }
            catch (Exception e) { throw e; }
            return _categoryPresenter.ContentResult;
        }


        // POST api/Categories
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto request)
        {
            Category cat = _mapper.Map<CategoryDto, Category>(request);

            var req = new CreateCategoryRequest(cat.CategoryCode, cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh, request.ParentCategory);

            await _createCategoryUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false);
            return _categoryPresenter.ContentResult;
        }

        //DELETE api/categories/{slug}
        [HttpDelete("{slug}")]
        [Authorize(Policies.SUPERADMIN)]
        public async Task<IActionResult> Delete(string slug)
        {
            var req = new DeleteCategoryRequest(slug);
            await _deleteCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        //PUT api/categories/{slug}
        [HttpPut("{slug}")]
        public async Task<IActionResult> UpdateCategory(string slug, CategoryDto Updated)
        {
            Category updated = _mapper.Map<Category>(Updated);
            var req = new UpdateCategoryRequest(slug, updated);
            await _updateCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        #endregion

        #region subcategory
        //GET api/categories/{slug}/subcategories

        [HttpGet("{slug}/subcategories")]
        public async Task<IActionResult> GetSubcategories([FromRoute]string slug)
        {
            var req = new GetAllSubcategoryRequest(slug);

            await _getAllSubcategoryUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false);
            return _categoryPresenter.ContentResult;
        }


        //GET api/categories/{slug}/subcategories/{slug}
        //TODO Return with all listings
        [HttpGet("{cat}/subcategories/{slug}")]
        public async Task<IActionResult> GetSubcategory([FromRoute]string cat, [FromRoute]string slug)
        {
            var req = new GetCategoryByNameRequest(cat, slug);
            await _getCategoryByNameUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false);
            return _categoryPresenter.ContentResult;
        }


        //POST api/categories/{slug}/subcategories
        [HttpPost("{slug}/subcategories")]
        public async Task<IActionResult> CreateSubcategory([FromRoute] string slug, [FromBody] CategoryDto request)
        {
            try
            {
                Category cat = _mapper.Map<CategoryDto, Category>(request);

                var req = new CreateCategoryRequest(cat.CategoryCode, cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh, slug);


                await _createCategoryUseCase.Handle(req, _categoryPresenter).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                throw e;
            }
            return _categoryPresenter.ContentResult;
        }
        //DELETE api/categories/{slug}/subcategories/{slug}
        [HttpDelete("{cat}/subcategories/{slug}")]
        public async Task<IActionResult> Delete([FromRoute] string cat, [FromRoute]string slug)
        {
            var req = new DeleteSubcategoryRequest(cat, slug);
            await _deleteSubcategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        //PUT api/categories/{slug}/subcategories/{slug}
        // [HttpPut("{cat}/subcategories/{slug}")]
        // public async Task<IActionResult> UpdateSubcategory([FromRoute]string cat, [FromRoute]string slug, [FromBody]CategoryDto Updated)
        // {
        //     Category updated = _mapper.Map<Category>(Updated);
        //     var req = new UpdateCategoryRequest(cat, slug, updated);
        //     await _updateSubcategoryUseCase.Handle(req, _categoryPresenter);
        //     return _categoryPresenter.ContentResult;
        // }

        #endregion
    }
}