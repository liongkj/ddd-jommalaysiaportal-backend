
using System.Threading.Tasks;
using AutoMapper;
using JomMalaysia.Api.UseCases.Categories.CreateCategory;
using JomMalaysia.Api.UseCases.Categories.DeleteCategory;
using JomMalaysia.Api.UseCases.Categories.GetCategory;
using JomMalaysia.Api.UseCases.Categories.UpdateCategory;
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
        private readonly CreateCategoryPresenter _createCategoryPresenter;
        private readonly IGetAllCategoryUseCase _getAllCategoryUseCase;
        private readonly GetAllCategoryPresenter _getAllCategoryPresenter;
        private readonly IGetCategoryByIdUseCase _getCategoryByIdUseCase;
        //private readonly IGetCategoryByNameUseCase _getCategoryByNameUseCase;
        private readonly GetCategoryPresenter _getCategoryPresenter;
        private readonly IDeleteCategoryUseCase _deleteCategoryUseCase;
        private readonly DeleteCategoryPresenter _deleteCategoryPresenter;
        private readonly IUpdateCategoryUseCase _updateCategoryUseCase;
        private readonly UpdateCategoryPresenter _updateCategoryPresenter;




        public CategoriesController(IMapper mapper,
        ICreateCategoryUseCase createCategoryUseCase, CreateCategoryPresenter CreateCategoryPresenter,
            IGetAllCategoryUseCase getAllCategoryUseCase, GetAllCategoryPresenter getAllCategoryPresenter,
            IGetCategoryByIdUseCase getCategoryByIdUseCase, GetCategoryPresenter getCategoryPresenter,
            //IGetCategoryByNameUseCase getCategoryByNameUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase, DeleteCategoryPresenter deleteCategoryPresenter,
            IUpdateCategoryUseCase updateCategoryUseCase, UpdateCategoryPresenter updateCategoryPresenter
            )
        {
            _mapper = mapper;
            _createCategoryUseCase = createCategoryUseCase;
            _createCategoryPresenter = CreateCategoryPresenter;
            _getAllCategoryUseCase = getAllCategoryUseCase;
            _getAllCategoryPresenter = getAllCategoryPresenter;
            _getCategoryByIdUseCase = getCategoryByIdUseCase;
            // _getCategoryByNameUseCase = getCategoryByNameUseCase;
            _getCategoryPresenter = getCategoryPresenter;
            _deleteCategoryUseCase = deleteCategoryUseCase;
            _deleteCategoryPresenter = deleteCategoryPresenter;
            _updateCategoryUseCase = updateCategoryUseCase;
            _updateCategoryPresenter = updateCategoryPresenter;


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
            var req = new GetCategoryByIdRequest(id);
            _getCategoryByIdUseCase.Handle(req, _getCategoryPresenter);
            return _getCategoryPresenter.ContentResult;
        }

        //Get api/categories/{name}
        //[HttpGet("{name:max(14)}")]
        //public IActionResult Get(string name)
        //{
        //    var req = new GetCategoryByNameRequest(name);
        //    _getCategoryByNameUseCase.Handle(req, _getCategoryPresenter);
        //    return _getCategoryPresenter.ContentResult;
        //}

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


        //PUT api/categories/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, CategoryDto Updated)
        {
            Category updated = _mapper.Map<Category>(Updated);
            var req = new UpdateCategoryRequest(id, updated);
            _updateCategoryUseCase.Handle(req, _updateCategoryPresenter);
            return _updateCategoryPresenter.ContentResult;
        }
    }
}