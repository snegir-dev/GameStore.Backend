using MediatR;

namespace GameStore.Application.CQs.Game.Commands.Purchase;

public class PurchaseGameCommand : IRequest<long>
{
    public long? UserId { get; set; }
    public long GameId { get; set; }
}