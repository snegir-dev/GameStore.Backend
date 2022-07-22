using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Company.Commands.Create;

namespace GameStore.WebApi.Models.Company;

public class CreateCompanyDto : IMapWith<CreateCompanyCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateCompanyDto, CreateCompanyCommand>()
            .ForMember(c => c.Name,
                o =>
                    o.MapFrom(c => c.Name));
    }
}