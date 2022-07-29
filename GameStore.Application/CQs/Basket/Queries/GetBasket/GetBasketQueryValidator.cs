using FluentValidation;

namespace GameStore.Application.CQs.Basket.Queries.GetBasket;

public class GetBasketQueryValidator : AbstractValidator<GetBasketQuery>
{
    public GetBasketQueryValidator()
    {
        RuleFor(b => b.Id).NotEmpty();
        RuleFor(b => b.UserId).NotEmpty();
    }
}