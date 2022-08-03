using MediatR;

namespace GameStore.Application.CQs.Game.Queries.GetGame;

public class GetGameQuery : IRequest<GameVm>
{
    public long Id { get; set; }
}