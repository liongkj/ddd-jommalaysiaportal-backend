
using FluentValidation;
using JomMalaysia.Core.UseCases.CatogoryUseCase.Create;

namespace JomMalaysia.Core.UseCases.CategoryUseCase.Create
{
    public class CreateCategoryRequestValidator : AbstractValidator<CreateCategoryRequest>
    {
        public CreateCategoryRequestValidator()
        {
            RuleFor(x => x.CategoryName)
            .NotEmpty()
            .WithMessage("{PropertyName} must not be Empty");
        }
    }
}