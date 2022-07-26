using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Domain;

namespace GameStore.Application.CQs.Basket.Queries.GetBasket;

public class BasketVm : IMapWith<Domain.Basket>
{
    public Domain.Game Game { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Basket, BasketVm>()
            .ForMember(b => b.Game,
                o =>
                    o.MapFrom(b => b.Game));
    }
}