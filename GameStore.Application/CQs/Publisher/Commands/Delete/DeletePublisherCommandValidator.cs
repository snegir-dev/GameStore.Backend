using FluentValidation;

namespace GameStore.Application.CQs.Publisher.Commands.Delete;

public class DeletePublisherCommandValidator : AbstractValidator<DeletePublisherCommand>
{
    public DeletePublisherCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
    }
}