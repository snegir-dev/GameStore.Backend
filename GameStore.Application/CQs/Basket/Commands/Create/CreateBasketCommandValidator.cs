using FluentValidation;

namespace GameStore.Application.CQs.Basket.Commands.Create;

public class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(b => b.GameId).NotEmpty();
        RuleFor(b => b.UserId).NotEmpty();
    }
}