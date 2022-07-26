using MediatR;

namespace GameStore.Application.CQs.Basket.Commands.Delete;

public class DeleteBasketCommand : IRequest
{
    public long Id { get; set; }
    public long? UserId { get; set; }
}