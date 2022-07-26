using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Basket.Commands.Update;

namespace GameStore.WebApi.Models.Basket;

public class UpdateBasketDto : IMapWith<UpdateBasketCommand>
{
    public long GameId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateBasketDto, UpdateBasketCommand>()
            .ForMember(b => b.GameId,
                o =>
                    o.MapFrom(b => b.GameId));
    }
}