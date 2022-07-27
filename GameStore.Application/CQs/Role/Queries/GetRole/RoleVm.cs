using AutoMapper;
using GameStore.Application.Common.Mappings;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.Role.Queries.GetRole;

public class RoleVm : IMapWith<IdentityRole<long>>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string ConcurrencyStamp { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<IdentityRole<long>, RoleVm>()
            .ForMember(r => r.Id,
                o =>
                    o.MapFrom(r => r.Id))
            .ForMember(r => r.Name,
                o =>
                    o.MapFrom(r => r.Name))
            .ForMember(r => r.ConcurrencyStamp,
                o =>
                    o.MapFrom(r => r.ConcurrencyStamp));
    }
}