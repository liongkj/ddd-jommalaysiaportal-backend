
using System;
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
            try
            {
                var response = await _CategoryRepository.FindByIdAsync(message.CategoryId);
                if (!response.Success)
                {
                    outputPort.Handle(new GetCategoryResponse(response.Errors));
                    return false;
                }

                outputPort.Handle(new GetCategoryResponse(response.Data, true));
                return response.Success;

            }
            catch (Exception e)
            {
                throw e;
            }


        }
    }
}
