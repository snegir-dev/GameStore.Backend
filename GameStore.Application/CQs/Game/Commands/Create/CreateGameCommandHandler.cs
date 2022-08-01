using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Game.Commands.Create;

public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, long>
{
    private readonly IGameStoreDbContext _context;

    public CreateGameCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateGameCommand request,
        CancellationToken cancellationToken)
    {
        var company = await _context.Companies
            .FirstOrDefaultAsync(c => c.Id == request.CompanyId,
                cancellationToken);
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.PublisherId,
                cancellationToken);

        var notFoundIds = new List<long>();
        var genres = _context.Genres
            .ToList()
            .Select(g =>
            {
                var isFound = request.GenreIds.Any(l => l == g.Id);
                if (isFound)
                {
                    notFoundIds.Add(g.Id);
                    return g;
                }

                return null;
            })
            .Where(g => g != null)
            .ToList();

        var differenceIds = request.GenreIds.Except(notFoundIds).ToList();

        if (differenceIds.Count > 0)
            throw new NotFoundException(nameof(Domain.Genre), differenceIds[0]);
        if (company == null)
            throw new NotFoundException(nameof(Company), request.CompanyId);
        if (publisher == null)
            throw new NotFoundException(nameof(Publisher), request.PublisherId);

        var game = new Domain.Game()
        {
            Name = request.Name,
            Title = request.Title,
            Description = request.Description,
            DateRelease = request.DateRelease,
            Price = request.Price,
            Company = company,
            Publisher = publisher,
            Genres = genres
        };
        await _context.Games.AddAsync(game, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return game.Id;
    }
}