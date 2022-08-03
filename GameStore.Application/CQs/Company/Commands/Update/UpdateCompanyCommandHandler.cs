using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Common.Models;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GameStore.Application.CQs.Company.Commands.Update;

public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly ICacheManager<Domain.Company> _cacheManager;

    public UpdateCompanyCommandHandler(IGameStoreDbContext context, 
        ICacheManager<Domain.Company> cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(UpdateCompanyCommand request, 
        CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .Include(c => c.Games)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (company == null)
            throw new NotFoundException(nameof(Domain.Company), request.Id);

        company.Name = request.Name;
        company.Description = request.Description;
        company.DateFoundation = request.DateFoundation;

        await _context.SaveChangesAsync(cancellationToken);
        
        _cacheManager.CacheEntryOptions = CacheEntryOption.DefaultCacheEntry;
        _cacheManager.ChangeCacheValue(request.Id, company);

        return Unit.Value;
    }
}