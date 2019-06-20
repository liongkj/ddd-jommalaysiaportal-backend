using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Interfaces.UseCases.Categories;
using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.CategoryUseCase
{
    public class GetAllCategoryUseCase : IGetAllCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetAllCategoryUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public async Task<bool> Handle(GetAllCategoryRequest message, IOutputPort<GetAllCategoryResponse> outputPort)
        {
            var response = await _CategoryRepository.GetAllCategories();
            foreach(var c in response.Categories){
                foreach(var sub in message.Subcategories)
                c.Subcategories.Add(sub);
            }
            if (!response.Success)
            {
                outputPort.Handle(new GetAllCategoryResponse(response.Errors));
            }
            outputPort.Handle(new GetAllCategoryResponse(response.Categories, true));

            return response.Success;
            //throw new NotImplementedException();
            //TODO 

        }
    }
}
