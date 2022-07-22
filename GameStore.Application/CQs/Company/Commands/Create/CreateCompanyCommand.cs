using GameStore.Domain;
using MediatR;

namespace GameStore.Application.CQs.Company.Commands.Create;

public class CreateCompanyCommand : IRequest<long>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }
}