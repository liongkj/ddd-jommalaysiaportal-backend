

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
        private readonly IGetCategoryByNameUseCase _getCategoryByNameUseCase;
        private readonly CategoryPresenter _categoryPresenter;
        
        private readonly IGetAllSubcategoryUseCase _getAllSubcategoryUseCase;
        private readonly IDeleteSubcategoryUseCase _deleteSubcategoryUseCase;
        private readonly IUpdateSubcategoryUseCase _updateSubcategoryUseCase;

        public CategoriesController(IMapper mapper,
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

        #region category

        //GET api/categories
        //get whole category collection
        [HttpGet]
        public IActionResult Get()
        {
            var req = new GetAllCategoryRequest();
            _getAllCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        //Get api/categories/{slug}
        [HttpGet("{slug}")]
        public IActionResult Get(string slug)
        {
            var req = new GetCategoryByNameRequest(slug);
            _getCategoryByNameUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        // POST api/Categories
        [HttpPost]
        public IActionResult Create([FromBody] CategoryDto request)
        {
            Category cat = _mapper.Map<CategoryDto, Category>(request);

            var req = new CreateCategoryRequest(cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh, request.ParentCategory);

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

        //TODO
        //PUT api/categories/{slug}
        [HttpPut("{slug}")]
        public IActionResult UpdateCategory(string slug, CategoryDto Updated)
        {
            Category updated = _mapper.Map<Category>(Updated);
            var req = new UpdateCategoryRequest(slug, updated);
            _updateCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        #endregion

        #region subcategory
        //GET api/categories/{slug}/subcategories

        [HttpGet("{slug}/subcategories")]
        public IActionResult GetSubcategories([FromRoute]string slug)
        {
            var req = new GetAllSubcategoryRequest(slug);

            _getAllSubcategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        //GET api/categories/{slug}/subcategories/{slug}
        [HttpGet("{cat}/subcategories/{slug}")]
        public IActionResult GetSubcategory([FromRoute]string cat, [FromRoute]string slug)
        {
            var req = new GetCategoryByNameRequest(cat, slug);
            _getCategoryByNameUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }


        //POST api/categories/{slug}
        [HttpPost("{slug}/subcategories")]
        public IActionResult CreateSubcategory([FromRoute] string slug, [FromBody] CategoryDto request)
        {
            Category cat = _mapper.Map<CategoryDto, Category>(request);

            var req = new CreateCategoryRequest(cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh, slug);

            _createCategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }
        //DELETE api/categories/{slug}/subcategories/{slug}
        [HttpDelete("{cat}/subcategories/{slug}")]
        public IActionResult Delete([FromRoute] string cat, [FromRoute]string slug)
        {
            var req = new DeleteSubcategoryRequest(cat, slug);
            _deleteSubcategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        //PUT api/categories/{slug}/subcategories/{slug}
        [HttpPut("{cat}/subcategories/{slug}")]
        public IActionResult UpdateSubcategory([FromRoute]string cat, [FromRoute]string slug, [FromBody]CategoryDto Updated)
        {
            Category updated = _mapper.Map<Category>(Updated);
            var req = new UpdateCategoryRequest(cat,slug, updated);
            _updateSubcategoryUseCase.Handle(req, _categoryPresenter);
            return _categoryPresenter.ContentResult;
        }

        #endregion
    }
}