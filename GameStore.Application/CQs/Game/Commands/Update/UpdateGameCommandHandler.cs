using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Game.Commands.Update;

public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public UpdateGameCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateGameCommand request, 
        CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId,
                cancellationToken);
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.PublisherId,
                cancellationToken);
        var genres = _context.Genres
            .ToList()
            .Select(g =>
            {
                var nonexistentId = request.GenreIds
                    .FirstOrDefault(l => l != g.Id);

                if (nonexistentId != default)
                    throw new NotFoundException(nameof(Domain.Genre), nonexistentId);

                return g;
            })
            .ToList();

        var changeGame = await _context.Games
            .Include(g => g.Company)
            .Include(g => g.Publisher)
            .Include(g => g.Genres)
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);

        if (company == null)
            throw new NotFoundException(nameof(Company), request.CompanyId);
        if (publisher == null)
            throw new NotFoundException(nameof(Publisher), request.PublisherId);
        if (changeGame == null)
            throw new NotFoundException(nameof(Domain.Game), request.Id);

        changeGame.Name = request.Name;
        changeGame.Title = request.Title;
        changeGame.Description = request.Description;
        changeGame.DateRelease = request.DateRelease;
        changeGame.Price = request.Price;
        changeGame.Company = company;
        changeGame.Publisher = publisher;
        changeGame.Genres = genres;
        
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}