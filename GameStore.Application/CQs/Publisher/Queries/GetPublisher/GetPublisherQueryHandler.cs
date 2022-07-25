using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Publisher.Queries.GetPublisher;

public class GetPublisherQueryHandler : IRequestHandler<GetPublisherQuery, PublisherVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetPublisherQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PublisherVm> Handle(GetPublisherQuery request, 
        CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);
        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);

        var vm = _mapper.Map<PublisherVm>(publisher);

        return vm;
    }
}