using MediatR;

namespace GameStore.Application.CQs.Basket.Commands.Update;

public class UpdateBasketCommand : IRequest
{
    public long Id { get; set; }
    public long GameId { get; set; }
    public long? UserId { get; set; }
}