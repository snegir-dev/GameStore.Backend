using FluentValidation;

namespace GameStore.Application.CQs.Genre.Commands.Update;

public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
{
    public UpdateGenreCommandValidator()
    {
        RuleFor(g => g.Id).NotEmpty();
        RuleFor(g => g.Name).NotEmpty().MaximumLength(255);
    }
}