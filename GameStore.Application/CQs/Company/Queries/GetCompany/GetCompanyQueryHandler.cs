using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Company.Queries.GetCompany;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;

    public GetCompanyQueryHandler(IGameStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CompanyVm> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .Include(c => c.Games)
            .ThenInclude(g => g.Publisher)
            .Include(c => c.Games)
            .ThenInclude(g => g.Genres)
            .Include(c => c.Games)
            .ThenInclude(g => g.Users)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (company == null)
            throw new NotFoundException(nameof(Company), request.Id);

        return _mapper.Map<CompanyVm>(company);
    }
}