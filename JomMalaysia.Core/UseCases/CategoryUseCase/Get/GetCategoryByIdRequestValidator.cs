
using FluentValidation;
using JomMalaysia.Core.Interfaces;

namespace JomMalaysia.Core.UseCases.CatogoryUseCase.Get
{
    public class GetCategoryByIdRequestValidator : AbstractValidator<GetCategoryByIdRequest>
    {

        public GetCategoryByIdRequestValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
        }

    }
}
