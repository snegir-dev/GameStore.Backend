using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Company.Commands.Create;
using GameStore.Application.CQs.Publisher.Commands.Create;

namespace GameStore.WebApi.Models.Company;

public class CreateCompanyDto : IMapWith<CreateCompanyCommand>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCompanyDto, CreateCompanyCommand>()
            .ForMember(c => c.Name,
                o =>
                    o.MapFrom(c => c.Name))
            .ForMember(c => c.Description,
                o =>
                    o.MapFrom(c => c.Description))
            .ForMember(c => c.DateFoundation,
                o =>
                    o.MapFrom(c => c.DateFoundation));
    }
}