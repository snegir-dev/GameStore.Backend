using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GameStore.Application.CQs.Game.Commands.Update;

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly ICacheManager<Domain.Game> _cacheManager;

    public UpdateGameCommandHandler(IGameStoreDbContext context, 
        ICacheManager<Domain.Game> cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(UpdateGameCommand request, 
        CancellationToken cancellationToken)
    {
        var changeGame = await _context.Games
            .Include(g => g.Company)
            .Include(g => g.Publisher)
            .Include(g => g.Genres)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (changeGame == null)
            throw new NotFoundException(nameof(Domain.Game), request.Id);
        
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId,
                cancellationToken);
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.PublisherId,
                cancellationToken);
        
        var notFoundIds = new List<long>();
        var genres = _context.Genres
            .ToList()
            .Select(g =>
            {
                var isFound = request.GenreIds.Any(l => l == g.Id);
                if (isFound)
                {
                    notFoundIds.Add(g.Id);
                    return g;
                }

                return null;
            })
            .Where(g => g != null)
            .ToList();
        
        var differenceIds = request.GenreIds.Except(notFoundIds).ToList();

        if (differenceIds.Count > 0)
            throw new NotFoundException(nameof(Domain.Genre), differenceIds[0]);
        if (company == null)
            throw new NotFoundException(nameof(Company), request.CompanyId);
        if (publisher == null)
            throw new NotFoundException(nameof(Publisher), request.PublisherId);

        changeGame.Name = request.Name;
        changeGame.Title = request.Title;
        changeGame.Description = request.Description;
        changeGame.DateRelease = request.DateRelease;
        changeGame.Price = request.Price;
        changeGame.Company = company;
        changeGame.Publisher = publisher;
        changeGame.Genres = genres;

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, changeGame);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}