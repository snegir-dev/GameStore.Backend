using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Domain;

namespace GameStore.Application.CQs.Company.Queries.GetCompany;

public class CompanyVm : IMapWith<Domain.Company>
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<Game> Games { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Company, CompanyVm>()
            .ForMember(c => c.Id,
                o =>
                    o.MapFrom(c => c.Id))
            .ForMember(c => c.Name,
                o =>
                    o.MapFrom(c => c.Name))
            .ForMember(c => c.Games,
                o =>
                    o.MapFrom(c => c.Games));
    }
}