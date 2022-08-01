using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GameStore.Application.CQs.Genre.Queries.GetGenre;

public class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, GenreVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Genre> _cacheManager;

    public GetGenreQueryHandler(IGameStoreDbContext context, 
        IMapper mapper, ICacheManager<Domain.Genre> cacheManager)
    {
        _context = context;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<GenreVm> Handle(GetGenreQuery request, 
        CancellationToken cancellationToken)
    {
        var genreQuery = async () => await _context.Genres
            .Include(c => c.Games)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var genre = await _cacheManager.GetOrSetCacheValue(request.Id, genreQuery);

        return _mapper.Map<GenreVm>(genre);
    }
}