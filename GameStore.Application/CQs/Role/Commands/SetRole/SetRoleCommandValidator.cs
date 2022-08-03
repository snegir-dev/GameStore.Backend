using FluentValidation;

namespace GameStore.Application.CQs.Role.Commands.SetRole;

public class SetRoleCommandValidator : AbstractValidator<SetRoleCommand>
{
    public SetRoleCommandValidator()
    {
        RuleFor(r => r.RoleId).NotEmpty();
        RuleFor(r => r.UserId).NotEmpty();
    }
}