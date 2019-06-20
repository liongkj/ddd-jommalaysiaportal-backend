
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Api.UseCases.Categories.CreateCategory;
using JomMalaysia.Core.Domain.Entities;
using JomMalaysia.Core.Interfaces.UseCases.Categories;
using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JomMalaysia.Api.UseCases.Categories.DeleteCategory;
using JomMalaysia.Api.UseCases.Categories.GetCategory;
using JomMalaysia.Api.UseCases.Categories.UpdateCategory;

namespace JomMalaysia.Api.UseCases.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICreateCategoryUseCase _createCategoryUseCase;
        private readonly CreateCategoryPresenter _createCategoryPresenter;
        private readonly IGetAllCategoryUseCase _getAllCategoryUseCase;
        private readonly GetAllCategoryPresenter _getAllCategoryPresenter;
        private readonly IGetCategoryUseCase _getCategoryUseCase;
        private readonly GetCategoryPresenter _getCategoryPresenter;
        private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;
        private readonly DeleteCategoryPresenter _deleteCategoryPresenter;
        private readonly IUpdateCategoryUseCase _updateCategoryUseCase;
        private readonly UpdateCategoryPresenter _updateCategoryPresenter;
        private readonly ICreateSubcategoryUseCase _createSubcategoryUseCase;



        public CategoriesController(IMapper mapper,
        ICreateCategoryUseCase createCategoryUseCase, CreateCategoryPresenter CreateCategoryPresenter,
            IGetAllCategoryUseCase getAllCategoryUseCase, GetAllCategoryPresenter getAllCategoryPresenter,
            IGetCategoryUseCase getCategoryUseCase, GetCategoryPresenter getCategoryPresenter,
            IDeleteCategoryUseCase deleteCategoryUseCase, DeleteCategoryPresenter deleteCategoryPresenter,
            IUpdateCategoryUseCase updateCategoryUseCase, UpdateCategoryPresenter updateCategoryPresenter,
            ICreateSubcategoryUseCase createSubcategoryUseCase
            )
        {
            _mapper = mapper;
            _createCategoryUseCase = createCategoryUseCase;
            _createCategoryPresenter = CreateCategoryPresenter;
            _getAllCategoryUseCase = getAllCategoryUseCase;
            _getAllCategoryPresenter = getAllCategoryPresenter;
            _getCategoryUseCase = getCategoryUseCase;
            _getCategoryPresenter = getCategoryPresenter;
            _deleteCategoryUseCase = deleteCategoryUseCase;
            _deleteCategoryPresenter = deleteCategoryPresenter;
            _updateCategoryUseCase = updateCategoryUseCase;
            _updateCategoryPresenter = updateCategoryPresenter;
            _createSubcategoryUseCase = createSubcategoryUseCase;
        }
        //GET api/categories
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var req = new GetAllCategoryRequest();
            await _getAllCategoryUseCase.Handle(req, _getAllCategoryPresenter);

            return _getAllCategoryPresenter.ContentResult;
        }

        //Get api/categories/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var req = new GetCategoryRequest(id);
            _getCategoryUseCase.Handle(req, _getCategoryPresenter);
            return _getCategoryPresenter.ContentResult;
        }

        // POST api/Categories
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Category cat = _mapper.Map<CategoryDto, Category>(request);

            var req = new CreateCategoryRequest(cat.CategoryName, cat.CategoryNameMs, cat.CategoryNameZh);

            _createCategoryUseCase.Handle(req, _createCategoryPresenter);
            return _createCategoryPresenter.ContentResult;
        }

        //DELETE api/categories/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var req = new DeleteCategoryRequest(id);
            _deleteCategoryUseCase.Handle(req, _deleteCategoryPresenter);
            return _deleteCategoryPresenter.ContentResult;
        }

        //subcategory
        //PUT api/categories/{id}
        [HttpPut("{id}")]
        public IActionResult AddSubcategory(string id, [FromBody] SubcategoryDto request)
        {
            var req = new CreateSubcategoryRequest(id, request.SubcategoryName, request.SubcategoryNameMs, request.SubcategoryNameZh);
            _createSubcategoryUseCase.Handle(req, _updateCategoryPresenter);
            return _updateCategoryPresenter.ContentResult;
        }
    }
}