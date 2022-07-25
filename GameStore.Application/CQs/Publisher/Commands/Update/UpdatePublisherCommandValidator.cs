using FluentValidation;

namespace GameStore.Application.CQs.Publisher.Commands.Update;

public class UpdatePublisherCommandValidator : AbstractValidator<UpdatePublisherCommand>
{
    public UpdatePublisherCommandValidator()
    {
        RuleFor(p => p.Id).NotEmpty();
        RuleFor(p => p.Name).NotEmpty().MaximumLength(255);
        RuleFor(p => p.Description).NotEmpty().MaximumLength(600);
        RuleFor(p => p.DateFoundation).NotEmpty();
    }
}