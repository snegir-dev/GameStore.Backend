using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.User.Commands.ReplenishmentBalance;

namespace GameStore.WebApi.Models.User;

public class ReplenishmentBalanceDto : IMapWith<ReplenishmentBalanceCommand>
{
    public string CardNumber { get; set; }
    public string ExpirationDate { get; set; }
    public string CardCode { get; set; }
    public long ReplenishmentAmount { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ReplenishmentBalanceDto, ReplenishmentBalanceCommand>()
            .ForMember(r => r.CardNumber,
                o =>
                    o.MapFrom(r => r.CardNumber))
            .ForMember(r => r.ExpirationDate,
                o =>
                    o.MapFrom(r => r.ExpirationDate))
            .ForMember(r => r.CardCode,
                o =>
                    o.MapFrom(r => r.CardCode))
            .ForMember(r => r.ReplenishmentAmount,
                o =>
                    o.MapFrom(r => r.ReplenishmentAmount));
    }
}