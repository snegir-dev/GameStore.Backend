using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Basket.Commands.Delete;

public class DeleteBasketCommandHandler : IRequestHandler<DeleteBasketCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public DeleteBasketCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteBasketCommand request, 
        CancellationToken cancellationToken)
    {
        var basket = await _context.Baskets
            .Include(b => b.User)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);
        if (basket == null)
            throw new NotFoundException(nameof(Domain.Basket), request.Id);

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.UserId, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);

        if (basket.User.Id != user.Id)
            throw new Exception("Wrong user");
        
        _context.Baskets.Remove(basket);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}