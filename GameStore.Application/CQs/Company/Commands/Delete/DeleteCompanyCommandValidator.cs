using FluentValidation;

namespace GameStore.Application.CQs.Company.Commands.Delete;

public class DeleteCompanyCommandValidator : AbstractValidator<DeleteCompanyCommand>
{
    public DeleteCompanyCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}