using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace GameStore.Application.CQs.Company.Commands.Delete;

public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly ICacheManager<Domain.Company> _cacheManager;

    public DeleteCompanyCommandHandler(IGameStoreDbContext context,
        ICacheManager<Domain.Company> cacheManager)
    {
        _context = context;
        _cacheManager = cacheManager;
    }

    public async Task<Unit> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (company == null)
            throw new NotFoundException(nameof(Company), request.Id);

        _context.Companies.Remove(company);
        await _context.SaveChangesAsync(cancellationToken);
        _cacheManager.RemoveCacheValue(request.Id);
        
        return Unit.Value;
    }
}