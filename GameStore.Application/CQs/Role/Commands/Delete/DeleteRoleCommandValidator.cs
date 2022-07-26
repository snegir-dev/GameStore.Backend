using FluentValidation;

namespace GameStore.Application.CQs.Role.Commands.Delete;

public class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
    }
}