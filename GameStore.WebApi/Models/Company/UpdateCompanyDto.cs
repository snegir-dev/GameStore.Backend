using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Company.Commands.Update;
using GameStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Models.Company;

public class UpdateCompanyDto : IMapWith<UpdateCompanyCommand>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateCompanyDto, UpdateCompanyCommand>()
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