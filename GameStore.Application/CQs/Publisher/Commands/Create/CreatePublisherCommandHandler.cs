using GameStore.Application.Interfaces;
using MediatR;

namespace GameStore.Application.CQs.Publisher.Commands.Create;

public class CreatePublisherCommandHandler : IRequestHandler<CreatePublisherCommand, long>
{
    private readonly IGameStoreDbContext _context;

    public CreatePublisherCommandHandler(IGameStoreDbContext context)
    {
        _context = context;
    }

    public async Task<long> Handle(CreatePublisherCommand request, 
        CancellationToken cancellationToken)
    {
        var publisher = new Domain.Publisher()
        {
            Name = request.Name,
            Description = request.Description,
            DateFoundation = request.DateFoundation
        };

        await _context.Publishers.AddAsync(publisher, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return publisher.Id;
    }
}