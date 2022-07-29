using FluentValidation;

namespace GameStore.Application.CQs.Game.Commands.Delete;

public class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
{
    public DeleteGameCommandValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
    }
}