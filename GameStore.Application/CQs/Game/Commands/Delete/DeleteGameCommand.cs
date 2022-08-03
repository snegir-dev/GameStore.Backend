using MediatR;

namespace GameStore.Application.CQs.Game.Commands.Delete;

public class DeleteGameCommand : IRequest
{
    public long Id { get; set; }
}