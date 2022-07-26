using FluentValidation;

namespace GameStore.Application.CQs.Role.Commands.Create;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(r => r.Name).NotEmpty().MaximumLength(255);
    }
}