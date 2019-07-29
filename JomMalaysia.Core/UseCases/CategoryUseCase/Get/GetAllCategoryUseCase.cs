﻿using System.Threading.Tasks;
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetAllCategoryUseCase : IGetAllCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public GetAllCategoryUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public bool Handle(GetAllCategoryRequest message, IOutputPort<GetAllCategoryResponse> outputPort)
        {
            var response = _CategoryRepository.GetAllCategories(message.PageSize,message.PageNumber);
            //foreach(var c in response.Categories){
            //    foreach(var sub in message.Subcategories)
            //    c.Subcategories.Add(sub);
            //}
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
