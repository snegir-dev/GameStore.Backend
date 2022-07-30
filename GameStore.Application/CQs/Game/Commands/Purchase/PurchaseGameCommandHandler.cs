using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Game.Commands.Purchase;

public class PurchaseGameCommandHandler : IRequestHandler<PurchaseGameCommand, long>
{
    private readonly IGameStoreDbContext _context;

    public PurchaseGameCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(PurchaseGameCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Baskets)
            .Include(u => u.Games)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);

        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);
        if (game == null)
            throw new NotFoundException(nameof(Domain.Game), request.GameId);

        if (user.Balance < game.Price)
            throw new Exception($"Insufficient funds. Balance - {user.Balance}");

        var basket = user.Baskets.FirstOrDefault(b => b.Game == game);
        if (basket != null)
        {
            user.Baskets.Remove(basket);
        }

        user.Balance -= game.Price;
        user.Games.Add(game);
        
        await _context.SaveChangesAsync(cancellationToken);
        
        return game.Id;
    }
}