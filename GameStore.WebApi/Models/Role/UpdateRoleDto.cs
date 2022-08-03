using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Role.Commands.Update;
using Microsoft.AspNetCore.Identity;

namespace GameStore.WebApi.Models.Role;

public class UpdateRoleDto : IMapWith<UpdateRoleCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateRoleDto, UpdateRoleCommand>()
            .ForMember(r => r.Name,
                o =>
                    o.MapFrom(r => r.Name));
    }
}