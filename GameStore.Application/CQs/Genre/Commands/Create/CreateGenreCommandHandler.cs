using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Genre.Commands.Create;

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, long>
{
    private readonly IGameStoreDbContext _context;

    public CreateGenreCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreateGenreCommand request, 
        CancellationToken cancellationToken)
    {
        var isExistGenre = await _context.Genres
            .AnyAsync(g => g.Name == request.Name, cancellationToken);
        if (isExistGenre)
            throw new RecordExistsException(nameof(Domain.Genre), request.Name);

        var genre = new Domain.Genre()
        {
            Name = request.Name
        };
        
        await _context.Genres.AddAsync(genre, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return genre.Id;
    }
}