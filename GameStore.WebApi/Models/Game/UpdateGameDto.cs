using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Game.Commands.Update;

namespace GameStore.WebApi.Models.Game;

public class UpdateGameDto : IMapWith<UpdateGameCommand>
{
    public string Name { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateOnly DateRelease { get; set; }
    public decimal Price { get; set; }
    
    public long CompanyId { get; set; }
    public long PublisherId { get; set; }
    public long[] GenreIds { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateGameDto, UpdateGameCommand>()
            .ForMember(g => g.Name,
                o =>
                    o.MapFrom(g => g.Name))
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
            .ForMember(g => g.CompanyId,
                o =>
                    o.MapFrom(g => g.CompanyId))
            .ForMember(g => g.PublisherId,
                o =>
                    o.MapFrom(g => g.PublisherId))
            .ForMember(g => g.GenreIds,
                o =>
                    o.MapFrom(g => g.GenreIds));
    }
}