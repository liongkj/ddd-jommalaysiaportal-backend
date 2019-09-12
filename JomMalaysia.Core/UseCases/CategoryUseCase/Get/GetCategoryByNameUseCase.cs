
using System;
using System.Threading.Tasks;
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
        public async Task<bool> Handle(GetCategoryByNameRequest message, IOutputPort<GetCategoryResponse> outputPort)
        {
            try
            {
                if (message.ParentCategory == null)//is parent
                {
                    var response = await _CategoryRepository.GetCategoryAsync(message.Name).ConfigureAwait(false);
                    //return HandleResponseIsSuccess(response, outputPort);
                    outputPort.Handle(response);
                    return response.Success;
                }
                else //is subcategory
                {
                    var response = await _CategoryRepository.FindByNameAsync(message.ParentCategory, message.Name).ConfigureAwait(false);
                    //return HandleResponseIsSuccess(response, outputPort);
                    outputPort.Handle(response);
                    return response.Success;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
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
