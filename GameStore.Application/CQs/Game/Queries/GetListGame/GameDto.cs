using AutoMapper;
using GameStore.Application.Common.Mappings;

namespace GameStore.Application.CQs.Game.Queries.GetListGame;

public class GameDto : IMapWith<Domain.Game>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly DateRelease { get; set; }
    public decimal Price { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Game, GameDto>()
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
            .ForMember(g => g.Title, 
                o => 
                    o.MapFrom(g => g.Price));
    }
}