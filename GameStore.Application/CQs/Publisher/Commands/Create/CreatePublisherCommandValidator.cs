using FluentValidation;

namespace GameStore.Application.CQs.Publisher.Commands.Create;

public class CreatePublisherCommandValidator : AbstractValidator<CreatePublisherCommand>
{
    public CreatePublisherCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(255);
        RuleFor(p => p.Description).NotEmpty().MaximumLength(600);
        RuleFor(p => p.DateFoundation).NotEmpty();
    }
}