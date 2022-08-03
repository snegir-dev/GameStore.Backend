using GameStore.Application.Interfaces;
using MediatR;

namespace GameStore.Application.CQs.Company.Commands.Create;

public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, long>
{
    private readonly IGameStoreDbContext _context;

    public CreateCompanyCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var company = new Domain.Company()
        {
            Name = request.Name,
            Description = request.Description,
            DateFoundation = request.DateFoundation
        };
        
        await _context.Companies.AddAsync(company, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        
        return company.Id;
    }
}