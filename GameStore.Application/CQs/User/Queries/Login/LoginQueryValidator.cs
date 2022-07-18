using FluentValidation;

namespace GameStore.Application.CQs.User.Queries.Login;

public class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator()
    {
        RuleFor(q => q.Email).NotEmpty().MaximumLength(255);
        RuleFor(q => q.Email).NotEmpty().MaximumLength(255);
    }
}