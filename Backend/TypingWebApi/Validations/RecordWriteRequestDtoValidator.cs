using Domain.Models;
using FluentValidation;
using TypingWebApi.Dtos;

namespace TypingWebApi.Validations
{
    internal sealed class RecordWriteRequestDtoValidator : AbstractValidator<RecordWriteRequestDto>
    {
        public RecordWriteRequestDtoValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("UserId is required.")
                .Length(1, 50).WithMessage("UserId must be between 1 and 50 characters.");
            RuleFor(x => x.Wpm)
                .GreaterThanOrEqualTo(0).WithMessage("Wpm must be a non-negative integer.");
            RuleFor(x => x.Raw)
                .GreaterThanOrEqualTo(0).WithMessage("Raw must be a non-negative integer.");
            RuleFor(x => x.Accuracy)
                .InclusiveBetween(0, 100).WithMessage("Accuracy must be between 0 and 100.");
            RuleFor(x => x.Experience)
                .GreaterThanOrEqualTo(0).WithMessage("Experience must be a non-negative integer.");
            RuleFor(x => x.Language)
                .NotEmpty().WithMessage("Language is required.")
                .Must(lang => lang == "en" || lang == "uk").WithMessage("Language must be either 'en' or 'uk'.");
            RuleFor(x => x.MatchTime)
                .GreaterThanOrEqualTo(0).WithMessage("MatchTime must be a non-negative integer.");
            RuleFor(x => x.GameLength)
                .GreaterThan(0).WithMessage("GameLength must be greater than 0.");
            RuleFor(x => x.Mode)
                .IsInEnum().WithMessage("Mode must be a valid GameMode value.");
        }
    }
}
