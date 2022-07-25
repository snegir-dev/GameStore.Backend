using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Genre.Commands.Update;

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public UpdateGenreCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateGenreCommand request, 
        CancellationToken cancellationToken)
    {
        var isExistGenre = await _context.Genres
            .AnyAsync(g => g.Name == request.Name && 
                           g.Id != request.Id, cancellationToken);
        if (isExistGenre)
            throw new RecordExistsException(nameof(Domain.Genre), request.Name);
        
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (genre == null)
            throw new NotFoundException(nameof(Domain.Genre), request.Id);

        genre.Name = request.Name;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}