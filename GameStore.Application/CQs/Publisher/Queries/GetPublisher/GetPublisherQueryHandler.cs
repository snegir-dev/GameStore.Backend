using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Publisher.Queries.GetPublisher;

public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, PublisherVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Publisher> _cacheManager;

    public GetPublisherQueryHandler(IGameStoreDbContext context, 
        IMapper mapper, ICacheManager<Domain.Publisher> cacheManager)
    {
        _context = context;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<PublisherVm> Handle(GetPublisherQuery request, 
        CancellationToken cancellationToken)
    {
        var publisherQuery = async () => await _context.Publishers
            .Include(p => p.Games)
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        var publisher = await _cacheManager.GetOrSetCacheValue(request.Id, publisherQuery);

        foreach (var game in publisher.Games)
        {
            game.Baskets = null!;
            game.Publisher = null!;
            game.Users = null!;
        }

        var vm = _mapper.Map<PublisherVm>(publisher);

        return vm;
    }
}