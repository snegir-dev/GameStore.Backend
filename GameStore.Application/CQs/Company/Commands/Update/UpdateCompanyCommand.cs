using GameStore.Domain;
using MediatR;

namespace GameStore.Application.CQs.Company.Commands.Update;

public class UpdateCompanyCommand : IRequest
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }
}