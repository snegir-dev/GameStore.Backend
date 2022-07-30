using AutoMapper;
using GameStore.Application.Common.Mappings;

namespace GameStore.Application.CQs.User;

public class AuthenticatedResponse : IMapWith<Domain.User>
{
    public long Id { get; set; }
    public string UserName { set; get; }
    public string Email { get; set; }
    public decimal Balance { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.User, AuthenticatedResponse>()
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
                    o.MapFrom(u => u.Balance))
            .ForMember(u => u.RefreshToken,
                o =>
                    o.MapFrom(u => u.RefreshToken));
    }
}