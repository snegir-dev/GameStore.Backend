using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Role.Commands.SetRole;

namespace GameStore.WebApi.Models.Role;

public class SetRoleDto : IMapWith<SetRoleCommand>
{
    public long RoleId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<SetRoleDto, SetRoleCommand>()
            .ForMember(r => r.RoleId,
                o =>
                    o.MapFrom(r => r.RoleId));
    }
}