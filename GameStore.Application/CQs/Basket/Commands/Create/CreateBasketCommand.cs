using MediatR;

namespace GameStore.Application.CQs.Basket.Commands.Create;

public class CreateBasketCommand : IRequest<long>
{
    public long GameId { get; set; }
    public long? UserId { get; set; }
}