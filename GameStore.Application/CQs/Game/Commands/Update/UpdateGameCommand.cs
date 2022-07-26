using MediatR;

namespace GameStore.Application.CQs.Game.Commands.Update;

public class UpdateGameCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly DateRelease { get; set; }
    public decimal Price { get; set; }
    
    public long CompanyId { get; set; }
    public long PublisherId { get; set; }
    public long[] GenreIds { get; set; }
}