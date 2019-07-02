
using JomMalaysia.Core.Interfaces;
using JomMalaysia.Core.Interfaces.Repositories;


namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Update
{
    public class UpdateCategoryUseCase : IUpdateCategoryUseCase
    {
        private readonly ICategoryRepository _CategoryRepository;

        public UpdateCategoryUseCase(ICategoryRepository CategoryRepository)
        {
            _CategoryRepository = CategoryRepository;
        }
        public bool HandleAsync(UpdateCategoryRequest message, IOutputPort<UpdateCategoryResponse> outputPort)
        {
            //TODO
            //verify update??
            var response = _CategoryRepository.Update(message.CategoryId,message.Updated);

            outputPort.Handle(response.Success ? new UpdateCategoryResponse(response.Id, true) : new UpdateCategoryResponse(response.Errors));
            return response.Success;
        }

       
    }
}
