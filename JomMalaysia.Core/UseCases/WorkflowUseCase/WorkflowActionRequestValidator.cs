using FluentValidation;

namespace JomMalaysia.Core.UseCases.WorkflowUseCase
{
    public class WorkflowActionRequestValidator : AbstractValidator<WorkflowActionRequest>
    {
        public WorkflowActionRequestValidator()
        {
            RuleFor(x => x.Action).NotEmpty().NotNull();
            RuleFor(x => x.WorkflowId).NotEmpty().NotNull().Length(24).WithMessage("Please enter a valid workflow Id");
        }
    }
}