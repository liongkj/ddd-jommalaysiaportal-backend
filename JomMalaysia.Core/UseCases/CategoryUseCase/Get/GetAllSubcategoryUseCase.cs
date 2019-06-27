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
        public bool Handle(GetAllSubcategoryRequest message, IOutputPort<GetAllSubcategoryResponse> outputPort)
        {

            var response = _CategoryRepository.GetAllSubcategory(message.Id);
            if (!response.Success)
            {
                outputPort.Handle(new GetAllSubcategoryResponse(response.Errors));
            }
            if (response != null)
            {
                outputPort.Handle(new GetAllSubcategoryResponse(response.Subcategories, true));
                return response.Success;
            }
            else
            {
                outputPort.Handle(new GetAllSubcategoryResponse(response.Errors, false, "Category Deleted or Not Found"));
                return false;
            }
        }
    }
}
