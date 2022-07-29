using FluentValidation;

namespace GameStore.Application.CQs.User.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(u => u.Id).NotEmpty();
        RuleFor(u => u.Email).NotEmpty();
    }
}