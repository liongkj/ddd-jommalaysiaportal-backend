using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Get
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

            try
            {
                var response = await _CategoryRepository.GetAllCategoriesAsync(message.PageSize, message.PageNumber);
                outputPort.Handle(response);
                return response.Success;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
