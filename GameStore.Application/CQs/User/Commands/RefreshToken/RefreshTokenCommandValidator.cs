using FluentValidation;

namespace GameStore.Application.CQs.User.Commands.RefreshToken;

public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommand>
{
    public RefreshTokenCommandValidator()
    {
        RuleFor(t => t.Token).NotEmpty();
        RuleFor(t => t.RefreshToken).NotEmpty();
    }
}