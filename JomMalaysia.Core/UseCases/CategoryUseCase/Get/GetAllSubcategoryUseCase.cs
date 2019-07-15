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
        public bool Handle(GetAllSubcategoryRequest message, IOutputPort<GetAllCategoryResponse> outputPort)
        {
            if (message == null)
            {
                throw new System.ArgumentNullException(nameof(message));
            }

            var response = _CategoryRepository.GetAllCategories(message.CategoryName);
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
