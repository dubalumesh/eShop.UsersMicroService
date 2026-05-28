

using FluentValidation;
using UsersMicroService.Core.DTOs;

namespace UsersMicroService.Core.Validators
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(temp => temp.Email).NotEmpty().EmailAddress().WithMessage("Please enter valid email address.");
            RuleFor(temp => temp.FirstName).NotEmpty().WithMessage("First name is required.");
            RuleFor(temp => temp.LastName).NotEmpty().WithMessage("Last name is required.");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Minimum 8 characters required")
                .MaximumLength(100)
                .Matches("[A-Z]").WithMessage("Must contain at least one uppercase letter")
                .Matches("[a-z]").WithMessage("Must contain at least one lowercase letter")
                .Matches("[0-9]").WithMessage("Must contain at least one number")
                .Matches("[^a-zA-Z0-9]").WithMessage("Must contain at least one special character");
            RuleFor(x => x.ConfirmPassword)
               .NotEmpty().WithMessage("Confirm password is required")
               .Equal(x => x.Password).WithMessage("Passwords do not match");

        }


    }
}
