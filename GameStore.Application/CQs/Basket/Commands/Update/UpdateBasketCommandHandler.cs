using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Basket.Commands.Update;

public class UpdateBasketCommandHandler : IRequestHandler<UpdateBasketCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public UpdateBasketCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateBasketCommand request,
        CancellationToken cancellationToken)
    {
        var basket = await _context.Baskets
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (basket == null)
            throw new NotFoundException(nameof(Domain.Basket), request.Id);

        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);
        if (game == null)
            throw new NotFoundException(nameof(Domain.Game), request.GameId);

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);

        var isExistBasket = await _context.Baskets
            .Include(b => b.Game)
            .Include(b => b.User)
            .AnyAsync(b => b.Game.Id == game.Id &&
                           b.User.Id == request.UserId, cancellationToken);
        if (isExistBasket)
            throw new RecordExistsException(nameof(Domain.Basket), $"game.Id = {game.Id}");

        basket.Game = game;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}