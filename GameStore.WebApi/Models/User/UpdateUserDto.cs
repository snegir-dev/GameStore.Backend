using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.User.Commands.Update;

namespace GameStore.WebApi.Models.User;

public class UpdateUserDto : IMapWith<UpdateUserCommand>
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateUserDto, UpdateUserCommand>()
            .ForMember(u => u.UserName,
                o =>
                    o.MapFrom(u => u.UserName))
            .ForMember(u => u.Email,
                o =>
                    o.MapFrom(u => u.Email))
            .ForMember(u => u.Password,
                o =>
                    o.MapFrom(u => u.Password));
    }
}