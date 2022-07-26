using FluentValidation;

namespace GameStore.Application.CQs.Game.Commands.Create;

public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
{
    public CreateGameCommandValidator()
    {
        RuleFor(g => g.Name).NotEmpty().MaximumLength(255);
        RuleFor(g => g.Title).NotEmpty().MaximumLength(255);
        RuleFor(g => g.Description).NotEmpty().MaximumLength(600);
        RuleFor(g => g.Price).NotEmpty();
        RuleFor(g => g.DateRelease).NotEmpty();
        RuleFor(g => g.CompanyId).NotEmpty();
        RuleFor(g => g.PublisherId).NotEmpty();
        RuleFor(g => g.CompanyId).NotEmpty();
    }
}