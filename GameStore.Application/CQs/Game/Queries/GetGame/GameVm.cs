using AutoMapper;
using GameStore.Application.Common.Mappings;

namespace GameStore.Application.CQs.Game.Queries.GetGame;

public class GameVm : IMapWith<Domain.Game>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly DateRelease { get; set; }
    public decimal Price { get; set; }

    public Domain.Company Company { get; set; }
    public Domain.Publisher Publisher { get; set; }
    public List<Domain.Genre> Genres { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Game, GameVm>()
            .ForMember(g => g.Id,
                o =>
                    o.MapFrom(g => g.Id))
            .ForMember(g => g.Title,
                o =>
                    o.MapFrom(g => g.Title))
            .ForMember(g => g.Description,
                o =>
                    o.MapFrom(g => g.Description))
            .ForMember(g => g.DateRelease,
                o =>
                    o.MapFrom(g => g.DateRelease))
            .ForMember(g => g.Price,
                o =>
                    o.MapFrom(g => g.Price))
            .ForMember(g => g.Company,
                o =>
                    o.MapFrom(g => g.Company))
            .ForMember(g => g.Publisher,
                o =>
                    o.MapFrom(g => g.Publisher))
            .ForMember(g => g.Genres,
                o =>
                    o.MapFrom(g => g.Genres));
    }
}