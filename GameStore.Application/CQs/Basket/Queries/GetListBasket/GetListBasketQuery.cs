using MediatR;

namespace GameStore.Application.CQs.Basket.Queries.GetListBasket;

public class GetListBasketQuery : IRequest<GetListBasketVm>
{
    public long? UserId { get; set; }
}