using FluentValidation;

namespace GameStore.Application.CQs.User.Commands.Registration;

public class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator()
    {
        RuleFor(r => r.Email).EmailAddress().NotEmpty().MaximumLength(255);
        RuleFor(r => r.UserName).NotEmpty().MaximumLength(255);
        RuleFor(r => r.Password).NotEmpty().MaximumLength(255);
    }
}