using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Genre.Queries.GetGenre;

public class GetGenreQueryHandler : IRequestHandler<GetGenreQuery, GenreVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetGenreQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GenreVm> Handle(GetGenreQuery request, 
        CancellationToken cancellationToken)
    {
        var genre = await _context.Genres
            .Include(c => c.Games)
            .ThenInclude(g => g.Publisher)
            .Include(c => c.Games)
            .ThenInclude(g => g.Genres)
            .Include(c => c.Games)
            .ThenInclude(g => g.Users)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (genre == null)
            throw new NotFoundException(nameof(Domain.Genre), request.Id);

        return _mapper.Map<GenreVm>(genre);
    }
}