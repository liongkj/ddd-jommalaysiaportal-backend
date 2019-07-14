
using JomMalaysia.Core.Domain.ValueObjects;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryByNameUseCase : IGetCategoryByNameUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetCategoryByNameUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public bool Handle(GetCategoryByNameRequest message, IOutputPort<GetCategoryResponse> outputPort)
        {
            
            var response = _CategoryRepository.FindByName(message.Name);
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
