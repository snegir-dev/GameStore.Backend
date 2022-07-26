using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using GameStore.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Basket.Commands.Create;

public class CreateBasketCommandHandler : IRequestHandler<CreateBasketCommand, long>
{
    private readonly IGameStoreDbContext _context;

    public CreateBasketCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateBasketCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);

        var game = await _context.Games
            .FirstOrDefaultAsync(g => g.Id == request.GameId, cancellationToken);
        if (game == null)
            throw new NotFoundException(nameof(Game), request.GameId);

        var isExistBasket = await _context.Baskets
            .AnyAsync(b => b.Game.Id == game.Id, cancellationToken);
        if (isExistBasket)
            throw new RecordExistsException(nameof(Domain.Basket), $"game.Id = {game.Id}");

        var basket = new Domain.Basket()
        {
            Game = game,
            User = user
        };
        await _context.Baskets.AddAsync(basket, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return basket.Id;
    }
}