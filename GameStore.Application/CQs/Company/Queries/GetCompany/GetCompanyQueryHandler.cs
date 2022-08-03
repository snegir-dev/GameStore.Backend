using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GameStore.Application.CQs.Company.Queries.GetCompany;

public class GetCompanyQueryHandler : IRequestHandler<GetCompanyQuery, CompanyVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheManager<Domain.Company> _cacheManager;

    public GetCompanyQueryHandler(IGameStoreDbContext context, IMapper mapper, 
        ICacheManager<Domain.Company> cacheManager)
    {
        _context = context;
        _mapper = mapper;
        _cacheManager = cacheManager;
    }

    public async Task<CompanyVm> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var companyQuery = async () => await _context.Companies
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        var company = await _cacheManager.GetOrSetCacheValue(request.Id, companyQuery);
        
        company.Games.ForEach(g => g.Company = null!);
        
        return _mapper.Map<CompanyVm>(company);
    }
}