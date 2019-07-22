
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
            if (message.ParentCategory == null)//is parent
            {
                if (message.Name != null)//null check
                {
                    //option 1:return one category
                    //var response = _CategoryRepository.FindByName(message.Name);

                    //option 2: return category object with all sub
                    var response = _CategoryRepository.GetCategory(message.Name);
                    return HandleResponseIsSuccess(response, outputPort);
                }
            }
            else //is subcategory
            {
                if (message.Name != null)//null check
                {
                    var response = _CategoryRepository.FindByName(message.ParentCategory,message.Name);
                    return HandleResponseIsSuccess(response, outputPort);
                }
            }
            return false;
        }

        private bool HandleResponseIsSuccess(GetCategoryResponse response, IOutputPort<GetCategoryResponse> outputPort)
        {
            if (!response.Success)
            {
                outputPort.Handle(new GetCategoryResponse(response.Errors));
                return false;
            }
            if (response.Category != null)
            {
                outputPort.Handle(new GetCategoryResponse(response.Category, true));
                return response.Success;
            }
            if (response.Categories != null)
            {
                outputPort.Handle(new GetCategoryResponse(response.Categories, true));
                return response.Success;
            }
            else
            {
                outputPort.Handle(new GetCategoryResponse(response.Errors, false, "Category/Subcategory Deleted or Not Found"));
                return false;
            }
        }
    }
}
