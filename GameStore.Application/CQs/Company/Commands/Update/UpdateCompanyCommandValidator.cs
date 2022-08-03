using FluentValidation;

namespace GameStore.Application.CQs.Company.Commands.Update;

public class UpdateCompanyCommandValidator : AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty().MaximumLength(255);
        RuleFor(c => c.Description).NotEmpty().MaximumLength(600);
        RuleFor(c => c.DateFoundation).NotEmpty();
    }
}