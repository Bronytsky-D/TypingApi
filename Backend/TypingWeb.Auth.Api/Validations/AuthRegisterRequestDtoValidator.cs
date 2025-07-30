using FluentValidation;
using TypingWeb.Auth.Api.Dtos;

namespace TypingWeb.Auth.Api.Validations
{
    public class AuthRegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public AuthRegisterRequestDtoValidator() 
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full name is required.")
                .Length(1, 100).WithMessage("Full name must be between 1 and 100 characters.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
        }
    }
}
