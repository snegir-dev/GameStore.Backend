using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Game.Commands.Delete;

public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public DeleteGameCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteGameCommand request, 
        CancellationToken cancellationToken)
    {
        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (game == null)
            throw new NotFoundException(nameof(Domain.Game), request.Id);

        _context.Games.Remove(game);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}