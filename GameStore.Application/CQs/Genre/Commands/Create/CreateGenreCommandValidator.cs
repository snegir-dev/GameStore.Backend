using FluentValidation;

namespace GameStore.Application.CQs.Genre.Commands.Create;

public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
{
    public CreateGenreCommandValidator()
    {
        RuleFor(g => g.Name).NotEmpty().MaximumLength(255);
    }
}