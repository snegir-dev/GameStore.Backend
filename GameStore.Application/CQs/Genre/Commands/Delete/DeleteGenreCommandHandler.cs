using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Genre.Commands.Delete;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public DeleteGenreCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteGenreCommand request, 
        CancellationToken cancellationToken)
    {
        var genre = await _context.Genres
            .FirstOrDefaultAsync(g => g.Id == request.Id, cancellationToken);
        if (genre == null)
            throw new NotFoundException(nameof(Domain.Genre), request.Id);

        _context.Genres.Remove(genre);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}