using FluentValidation;

namespace GameStore.Application.CQs.Game.Commands.Update;

public class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
{
    public UpdateGameCommandValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
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