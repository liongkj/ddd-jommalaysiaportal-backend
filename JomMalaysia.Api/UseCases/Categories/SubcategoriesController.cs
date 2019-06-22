using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JomMalaysia.Api.UseCases.Categories.GetCategory;
using JomMalaysia.Api.UseCases.Categories.UpdateCategory;
using JomMalaysia.Core.Interfaces.UseCases.Categories;
using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Infrastructure.Data.MongoDb.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JomMalaysia.Api.UseCases.Categories
{
    [Route("api/Categories/{CategoryId}/[controller]")]
    [ApiController]
    public class SubcategoriesController : ControllerBase
    {
        private readonly GetCategoryPresenter _getCategoryPresenter;
        private readonly UpdateCategoryPresenter _updateCategoryPresenter;
        private readonly ICreateSubcategoryUseCase _createSubcategoryUseCase;
        private readonly IDeleteSubcategoryUseCase _deleteSubcategoryUseCase;
        private readonly IGetAllSubcategoryUseCase _getAllSubcategoryUseCase;
        //TODO Delete Sub?

        public SubcategoriesController(ICreateSubcategoryUseCase createSubcategoryUseCase, IDeleteSubcategoryUseCase deleteSubcategoryUseCase,
        IGetAllSubcategoryUseCase getAllSubcategoryUseCase,
        UpdateCategoryPresenter updateCategoryPresenter,
        GetCategoryPresenter getCategoryPresenter)
        {
            _getCategoryPresenter = getCategoryPresenter;
            _createSubcategoryUseCase = createSubcategoryUseCase;
            _deleteSubcategoryUseCase = deleteSubcategoryUseCase;
            _updateCategoryPresenter = updateCategoryPresenter;
            _getAllSubcategoryUseCase = getAllSubcategoryUseCase;
        }

        //Get api/categories/{catid}/Subcategories
        [HttpGet]
        public IActionResult Get(string CategoryId)
        {
            var req = new GetAllSubcategoryRequest(CategoryId);
            _getAllSubcategoryUseCase.Handle(req, _getCategoryPresenter);
            return _getCategoryPresenter.ContentResult;
        }

        //Get api/categories/{catid}/Subcategories/{SubcategoryName}
        //  [HttpGet("SubcategoryName:alpha")]
        //    public IActionResult Get(string SubcategoryName)
        //      {

        //        }


        //PUT api/categories/{catid}/subcategories/
        [HttpPost]
        public IActionResult AddSubcategory(string CategoryId, [FromBody] SubcategoryDto request)
        {
            var req = new CreateSubcategoryRequest(CategoryId, request.SubcategoryName, request.SubcategoryNameMs, request.SubcategoryNameZh);
            _createSubcategoryUseCase.Handle(req, _updateCategoryPresenter);
            return _updateCategoryPresenter.ContentResult;
        }

        //put api/categories/{id}
        [HttpPut("{id}")]
        public IActionResult DeleteSubcategory(string id)
        {
            var req = new DeleteSubcategoryRequest(id);
            _deleteSubcategoryUseCase.Handle(req, _updateCategoryPresenter);
            return _updateCategoryPresenter.ContentResult;
        }
    }
}