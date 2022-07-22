using FluentValidation;

namespace GameStore.Application.CQs.Company.Commands.Create;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(600);
        RuleFor(c => c.DateFoundation).NotEmpty();
    }
}