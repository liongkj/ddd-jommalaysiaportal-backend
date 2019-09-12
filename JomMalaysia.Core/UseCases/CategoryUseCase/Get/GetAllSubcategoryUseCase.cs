using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetAllSubcategoryUseCase : IGetAllSubcategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetAllSubcategoryUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public async Task<bool> Handle(GetAllSubcategoryRequest message, IOutputPort<GetAllCategoryResponse> outputPort)
        {
            if (message == null)
            {
                throw new System.ArgumentNullException(nameof(message));
            }

            var response = await _CategoryRepository.GetAllCategoriesAsync(message.CategoryName);

            outputPort.Handle(response);
            return response.Success;

        }
    }
}
