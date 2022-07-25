using FluentValidation;

namespace GameStore.Application.CQs.Genre.Commands.Delete;

public class DeleteGenreCommandValidator : AbstractValidator<DeleteGenreCommand>
{
    public DeleteGenreCommandValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
    }
}