using FluentValidation;
using TypingWeb.Auth.Api.Dtos;

namespace TypingWeb.Auth.Api.Validations
{
    public class AuthLoginRequestDtoValidator : AbstractValidator<LoginRequestDto>
    {
        public AuthLoginRequestDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
