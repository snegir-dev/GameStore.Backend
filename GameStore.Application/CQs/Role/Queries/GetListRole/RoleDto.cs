using AutoMapper;
using GameStore.Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.Role.Queries.GetListRole;

public class RoleDto : IMapWith<IdentityRole<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IdentityRole<long>, RoleDto>()
            .ForMember(r => r.Id,
                o =>
                    o.MapFrom(r => r.Id))
            .ForMember(r => r.Name,
                o =>
                    o.MapFrom(r => r.Name));
    }
}