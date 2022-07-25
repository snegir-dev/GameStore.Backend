using MediatR;

namespace GameStore.Application.CQs.Basket.Queries.GetBasket;

public class GetBasketQuery : IRequest<BasketVm>
{
    public long Id { get; set; }
    public long? UserId { get; set; }
}