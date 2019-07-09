
using System.Threading.Tasks;
using AutoMapper;

using JomMalaysia.Core.Domain.Entities;
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
            //IGetCategoryByNameUseCase getCategoryByNameUseCase,
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
            _categoryPresenter = categoryPresenter;
        }

        #region category

        //GET api/categories
        //get whole category collection
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var req = new GetAllCategoryRequest();
            await _getAllCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        //Get api/categories/{slug}
        [HttpGet("{slug}")]
        public IActionResult Get(string slug)
        {
            var req = new GetCategoryByIdRequest(slug);
            _getCategoryByIdUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        // POST api/Categories
        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category cat = _mapper.Map<CategoryDto, Category>(request);

            var req = new CreateCategoryRequest(cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh,cat.CategoryPath);

            _createCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        //DELETE api/categories/{slug}
        [HttpDelete("{slug}")]
        public IActionResult Delete(string slug)
        {
            var req = new DeleteCategoryRequest(slug);
            _deleteCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        //PUT api/categories/{slug}
        [HttpPut("{slug}")]
        public IActionResult Put(string slug, CategoryDto Updated)
        {
            Category updated = _mapper.Map<Category>(Updated);
            var req = new UpdateCategoryRequest(slug, updated);
            _updateCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        #endregion

        #region subcategory
        //GET api/categories/{slug}/subcategories
        //GET api/categories/{slug}/subcategories/{slug}
        //POST api/categories/{slug}
        //DELETE api/categories/{slug}/subcategories/{slug}
        //PUT api/categories/{slug}//subcategories/{slug}

        #endregion
    }
}