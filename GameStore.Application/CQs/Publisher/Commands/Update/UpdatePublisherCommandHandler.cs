using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Publisher.Commands.Update;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly ICacheManager<Domain.Publisher> _cacheManager;

    public UpdatePublisherCommandHandler(IGameStoreDbContext context, 
        ICacheManager<Domain.Publisher> cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(UpdatePublisherCommand request, 
        CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers
            .Include(p => p.Games)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);

        publisher.Name = request.Name;
        publisher.Description = request.Description;
        publisher.DateFoundation = request.DateFoundation;

        await _context.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, publisher);
        
        return Unit.Value;
    }
}