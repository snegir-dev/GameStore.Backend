using GameStore.Application.Common.Mappings;

namespace GameStore.Application.CQs.Company.Queries.GetListCompany;

public class GetListCompanyVm
{
    public IEnumerable<CompanyDto> Companies { get; set; }
}