using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Company.Queries.GetListCompany;

public class GetListCompanyQueryHandler : IRequestHandler<GetListCompanyQuery, GetListCompanyVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetListCompanyQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetListCompanyVm> Handle(GetListCompanyQuery request,
        CancellationToken cancellationToken)
    {
        var companies = await _context.Companies
            .Include(p => p.Games)
            .ProjectTo<CompanyDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new GetListCompanyVm() { Companies = companies };
    }
}