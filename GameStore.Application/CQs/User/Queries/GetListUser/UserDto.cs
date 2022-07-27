using AutoMapper;
using GameStore.Application.Common.Mappings;

namespace GameStore.Application.CQs.User.Queries.GetListUser;

public class UserDto : IMapWith<Domain.User>
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public string Role { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.User, UserDto>()
            .ForMember(u => u.Id,
                o =>
                    o.MapFrom(u => u.Id))
            .ForMember(u => u.UserName, 
                o => 
                    o.MapFrom(u => u.UserName))
            .ForMember(u => u.Email, 
                o => 
                    o.MapFrom(u => u.Email))
            .ForMember(u => u.Balance, 
                o => 
                    o.MapFrom(u => u.Balance));
    }
}