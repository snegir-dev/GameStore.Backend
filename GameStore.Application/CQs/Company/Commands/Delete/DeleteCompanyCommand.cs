using MediatR;

namespace GameStore.Application.CQs.Company.Commands.Delete;

public class DeleteCompanyCommand : IRequest
{
    public long Id { get; set; }
}