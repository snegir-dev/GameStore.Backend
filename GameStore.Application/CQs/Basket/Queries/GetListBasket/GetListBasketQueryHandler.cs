using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Basket.Queries.GetListBasket;

public class GetListBasketQueryHandler : IRequestHandler<GetListBasketQuery, GetListBasketVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetListBasketQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetListBasketVm> Handle(GetListBasketQuery request,
        CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new NotFoundException(nameof(Domain.User), null);

        var baskets = await _context.Baskets
            .Include(b => b.Game)
            .Include(b => b.User)
            .Where(b => b.User.Id == request.UserId.Value)
            .ProjectTo<BasketDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetListBasketVm() { Baskets = baskets };
    }
}