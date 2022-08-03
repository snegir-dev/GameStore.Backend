using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Genre.Commands.Update;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly ICacheManager<Domain.Genre> _cacheManager;

    public UpdateGenreCommandHandler(IGameStoreDbContext context, 
        ICacheManager<Domain.Genre> cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(UpdateGenreCommand request, 
        CancellationToken cancellationToken)
    {
        var isExistGenre = await _context.Genres
            .Include(g => g.Games)
            .AnyAsync(g => g.Name == request.Name && 
                           g.Id != request.Id, cancellationToken);
        if (isExistGenre)
            throw new RecordExistsException(nameof(Domain.Genre), request.Name);
        
        var genre = await _context.Genres
            .Include(g => g.Games)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (genre == null)
            throw new NotFoundException(nameof(Domain.Genre), request.Id);

        genre.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, genre);
        
        return Unit.Value;
    }
}