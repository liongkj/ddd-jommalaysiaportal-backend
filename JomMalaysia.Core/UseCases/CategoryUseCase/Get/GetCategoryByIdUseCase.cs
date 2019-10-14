
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryByIdUseCase : IGetCategoryByIdUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetCategoryByIdUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public async Task<bool> Handle(GetCategoryByIdRequest message, IOutputPort<GetCategoryResponse> outputPort)
        {

            var response = await _CategoryRepository.FindByIdAsync(message.Id);
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
