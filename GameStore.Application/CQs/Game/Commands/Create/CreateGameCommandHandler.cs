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