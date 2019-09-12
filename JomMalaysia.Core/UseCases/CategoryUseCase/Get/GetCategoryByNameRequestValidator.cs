
using FluentValidation;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Get;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Get
{
    public class GetCategoryByNameRequestValidator : AbstractValidator<GetCategoryByNameRequest>
    {
        public GetCategoryByNameRequestValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("{PropertyName} must not be Empty");
        }
    }
}