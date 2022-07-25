using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Basket.Queries.GetBasket;

public class GetBasketQueryHandler : IRequestHandler<GetBasketQuery, BasketVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetBasketQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BasketVm> Handle(GetBasketQuery request, 
        CancellationToken cancellationToken)
    {
        if (request.UserId == null)
            throw new NotFoundException(nameof(Domain.User), null);

        var basket = await _context.Baskets
            .Include(b => b.User)
            .Include(b => b.Games)
            .ThenInclude(g => g.Genres)
            .Include(b => b.Games)
            .ThenInclude(g => g.Company)
            .Include(b => b.Games)
            .ThenInclude(g => g.Publisher)
            .FirstOrDefaultAsync(b => b.Id == request.Id &&
                                      b.User.Id == request.UserId.Value, cancellationToken)
            ?? new Domain.Basket();

        basket.User = null!;

        return _mapper.Map<BasketVm>(basket);
    }
}