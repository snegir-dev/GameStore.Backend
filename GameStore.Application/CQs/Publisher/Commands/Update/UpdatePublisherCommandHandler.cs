using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.Publisher.Commands.Update;

public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand, Unit>
{
    private readonly IGameStoreDbContext _context;

    public UpdatePublisherCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdatePublisherCommand request, 
        CancellationToken cancellationToken)
    {
        var publisher = await _context.Publishers
            .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

        if (publisher == null)
            throw new NotFoundException(nameof(Domain.Publisher), request.Id);

        publisher.Name = request.Name;
        publisher.Description = request.Description;
        publisher.DateFoundation = request.DateFoundation;

        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
}