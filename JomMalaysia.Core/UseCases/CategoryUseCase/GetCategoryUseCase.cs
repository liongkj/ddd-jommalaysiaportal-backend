
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.Interfaces.UseCases.Categories;

using JomMalaysia.Core.Services.Categories.UseCaseRequests;
using JomMalaysia.Core.Services.Categories.UseCaseResponses;

namespace JomMalaysia.Core.UseCases.CategoryUseCase
{
    public class GetCategoryUseCase : IGetCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetCategoryUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public bool Handle(GetCategoryRequest message, IOutputPort<GetCategoryResponse> outputPort)
        {
            var response = _CategoryRepository.FindById(message.Id);
            if (!response.Success)
            {
                outputPort.Handle(new GetCategoryResponse(response.Errors));
            }
            if (response.Category != null)
            {
                outputPort.Handle(new GetCategoryResponse(response.Category, true));
                return response.Success;
            }
            else
            {
                outputPort.Handle(new GetCategoryResponse(response.Errors, false, "Category Deleted or Not Found"));
                return false;
            }
        }
    }
}
