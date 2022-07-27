using FluentValidation;

namespace GameStore.Application.CQs.Role.Commands.Update;

public class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(r => r.Id).NotEmpty();
        RuleFor(r => r.Name).NotEmpty().MaximumLength(255);
    }
}