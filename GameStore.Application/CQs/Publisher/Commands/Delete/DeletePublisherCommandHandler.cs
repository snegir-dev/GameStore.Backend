using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Publisher.Commands.Delete;

public class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly ICacheManager<Domain.Publisher> _cacheManager;

    public DeletePublisherCommandHandler(IGameStoreDbContext context, 
        ICacheManager<Domain.Publisher> cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
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
        _cacheManager.RemoveCacheValue(request.Id);
        
        return Unit.Value;
    }
}