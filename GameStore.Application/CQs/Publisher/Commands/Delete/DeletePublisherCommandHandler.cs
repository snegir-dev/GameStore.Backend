using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Publisher.Commands.Delete;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public DeletePublisherCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeletePublisherCommand request, 
        CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);

        _context.Publishers.Remove(publisher);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}