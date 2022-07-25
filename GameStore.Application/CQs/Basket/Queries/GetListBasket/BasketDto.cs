using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Domain;

namespace GameStore.Application.CQs.Basket.Queries.GetListBasket;

public class BasketDto : IMapWith<Domain.Basket>
{
    public long Id { get; set; }
    public List<Game> Games { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Basket, BasketDto>()
            .ForMember(b => b.Games,
                o =>
                    o.MapFrom(b => b.Games));
    }
}