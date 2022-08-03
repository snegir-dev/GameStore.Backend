using FluentValidation;

namespace GameStore.Application.CQs.Basket.Commands.Update;

public class UpdateBasketCommandValidator : AbstractValidator<UpdateBasketCommand>
{
    public UpdateBasketCommandValidator()
    {
        RuleFor(b => b.Id).NotEmpty();
        RuleFor(b => b.GameId).NotEmpty();
        RuleFor(b => b.UserId).NotEmpty();
    }
}