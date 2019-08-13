using System;
using FluentValidation;
using JomMalaysia.Core.Domain.ValueObjects;

namespace JomMalaysia.Core.Validation
{
    public class PublishStatusValidator: AbstractValidator<PublishStatus>
    {
        public PublishStatusValidator()
        {
            RuleFor(x => x.ValidityStart)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(BeAValidStartDate);
            RuleFor(x => x.ValidityEnd)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .Must(BeAValidEndDate);
            RuleFor(x => x.IsPublished).NotNull();

        }

        protected bool BeAValidStartDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int currentDay = DateTime.Now.Day;
            int currentMonth = DateTime.Now.Month;

            return (date.Year == currentYear && date.Month == currentMonth && date.Day == currentDay);
        }

        protected bool BeAValidEndDate(DateTime date)
        {
            int currentYear = DateTime.Now.Year;
            int currentDay = DateTime.Now.Day;
            int currentMonth = DateTime.Now.Month;

            //package year should be implemented
            int packageyear = 1;

            return (date.Year - currentYear == packageyear);
        }


    }
}
