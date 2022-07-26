using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Basket.Commands.Create;

namespace GameStore.WebApi.Models.Basket;

public class CreateBasketDto : IMapWith<CreateBasketCommand>
{
    public long GameId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateBasketDto, CreateBasketCommand>()
            .ForMember(b => b.GameId,
                o =>
                    o.MapFrom(b => b.GameId));
    }
}