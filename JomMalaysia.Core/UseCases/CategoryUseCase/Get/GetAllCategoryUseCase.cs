using System.Globalization;
using System;
using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;
using AutoMapper;
using System.Collections.Generic;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Get
{
    public class GetAllCategoryUseCase : IGetAllCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoryUseCase(ICategoryRepository CategoryRepository, IMapper mapper)
        {
            _CategoryRepository = CategoryRepository;
            _mapper = mapper;
        }
        public async Task<bool> Handle(GetAllCategoryRequest message, IOutputPort<GetAllCategoryResponse> outputPort)
        {

            try
            {
                var response = await _CategoryRepository.GetAllCategoriesAsync(message.PageSize, message.PageNumber);
                var vm = _mapper.Map<List<CategoryViewModel>>(response.Categories);
                var mapped = new GetAllCategoryResponse(vm, response.Success, response.Message);
                outputPort.Handle(mapped);
                return response.Success;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}
