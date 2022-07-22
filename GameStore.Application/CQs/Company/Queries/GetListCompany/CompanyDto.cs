using AutoMapper;
using GameStore.Application.Common.Mappings;

namespace GameStore.Application.CQs.Company.Queries.GetListCompany;

public class CompanyDto : IMapWith<Domain.Company>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Company, CompanyDto>()
            .ForMember(c => c.Id,
                o =>
                    o.MapFrom(c => c.Id))
            .ForMember(c => c.Name,
                o =>
                    o.MapFrom(c => c.Name));
    }
}