using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Genre.Commands.Delete;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly ICacheManager<Domain.Genre> _cacheManager;

    public DeleteGenreCommandHandler(IGameStoreDbContext context, 
        ICacheManager<Domain.Genre> cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(DeleteGenreCommand request, 
        CancellationToken cancellationToken)
    {
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (genre == null)
            throw new NotFoundException(nameof(Domain.Genre), request.Id);

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.RemoveCacheValue(request.Id);
        
        return Unit.Value;
    }
}