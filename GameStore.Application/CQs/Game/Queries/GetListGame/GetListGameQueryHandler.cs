using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Game.Queries.GetListGame;

public class GetListGameQueryHandler : IRequestHandler<GetListGameQuery, GetListGameVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetListGameQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetListGameVm> Handle(GetListGameQuery request,
        CancellationToken cancellationToken)
    {
        var games = await _context.Games
            .ProjectTo<GameDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetListGameVm() { Games = games };
    }
}