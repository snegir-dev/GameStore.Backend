using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GameStore.Application.CQs.Game.Queries.GetGame;

public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Game> _cacheManager;

    public GetGameQueryHandler(IGameStoreDbContext context, 
        IMapper mapper, ICacheManager<Domain.Game> cacheManager)
    {
        _context = context;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<GameVm> Handle(GetGameQuery request, 
        CancellationToken cancellationToken)
    {
        var gameQuery = async () => await _context.Games
            .Include(g => g.Company)
            .Include(g => g.Publisher)
            .Include(g => g.Genres)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var game = await _cacheManager
            .GetOrSetCacheValue(request.Id, gameQuery);

        game.Company.Games = null!;
        game.Publisher.Games = null!;
        game.Genres.ForEach(g => g.Games = null!);

        return _mapper.Map<GameVm>(game);
    }
}