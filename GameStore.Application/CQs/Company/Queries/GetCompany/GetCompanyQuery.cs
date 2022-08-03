using MediatR;

namespace GameStore.Application.CQs.Company.Queries.GetCompany;

public class GetCompanyQuery : IRequest<CompanyVm>
{
    public long Id { get; set; }
}