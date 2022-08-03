using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Publisher.Queries.GetListPublisher;

public class GetListPublisherQueryHandler : IRequestHandler<GetListPublisherQuery, GetListPublisherVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetListPublisherQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetListPublisherVm> Handle(GetListPublisherQuery request,
        CancellationToken cancellationToken)
    {
        var publishers = await _context.Publishers
            .ProjectTo<PublisherDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetListPublisherVm() { Publishers = publishers };
    }
}