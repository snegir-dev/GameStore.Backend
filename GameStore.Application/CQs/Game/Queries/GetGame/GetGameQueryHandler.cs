using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Game.Queries.GetGame;

public class GetGameQueryHandler : IRequestHandler<GetGameQuery, GameVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGameQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GameVm> Handle(GetGameQuery request, 
        CancellationToken cancellationToken)
    {
        var game = await _context.Games
            .Include(g => g.Company)
            .Include(g => g.Publisher)
            .Include(g => g.Genres)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (game == null)
            throw new NotFoundException(nameof(Domain.Game), request.Id);

        game.Company.Games = null!;
        game.Publisher.Games = null!;
        game.Genres.ForEach(g => g.Games = null!);

        return _mapper.Map<GameVm>(game);
    }
}