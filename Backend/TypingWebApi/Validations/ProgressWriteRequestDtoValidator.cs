using FluentValidation;
using TypingWebApi.Dtos;

namespace TypingWebApi.Validations
{
    internal sealed class ProgressWriteRequestDtoValidator : AbstractValidator<ProgressWriteRequestDto>
    {
        public ProgressWriteRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .Length(1, 50).WithMessage("UserId must be between 1 and 50 characters.");
            RuleFor(x => x.LessonId)
                .GreaterThan(0).WithMessage("LessonId must be greater than 0.");
            RuleFor(x => x.BestWpm)
                .GreaterThanOrEqualTo(0).WithMessage("BestWpm must be a non-negative integer.");
            RuleFor(x => x.BestRaw)
                .GreaterThanOrEqualTo(0).WithMessage("BestRaw must be a non-negative integer.");
            RuleFor(x => x.BestAccuracy)
                .InclusiveBetween(0, 100).WithMessage("BestAccuracy must be between 0 and 100.");
        } 
    }
}
