using GameStore.Domain;
using MediatR;

namespace GameStore.Application.CQs.Company.Commands.Create;

public class CreateCompanyCommand : IRequest<long>
{
    public long Id { get; set; }
    public string Name { get; set; }
}