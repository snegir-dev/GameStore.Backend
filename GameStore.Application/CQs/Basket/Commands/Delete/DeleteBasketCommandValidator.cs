using FluentValidation;

namespace GameStore.Application.CQs.Basket.Commands.Delete;

public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(b => b.Id).NotEmpty();
        RuleFor(b => b.UserId).NotEmpty();
    }
}