using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Genre.Queries.GetListGenre;

public class GetListGenreQueryHandler : IRequestHandler<GetListGenreQuery, GetListGenreVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetListGenreQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetListGenreVm> Handle(GetListGenreQuery request,
        CancellationToken cancellationToken)
    {
        var genres = await _context.Genres
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetListGenreVm() { Genres = genres };
    }
}