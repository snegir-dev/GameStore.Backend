using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Domain;

namespace GameStore.Application.CQs.Genre.Queries.GetGenre;

public class GenreVm : IMapWith<Domain.Genre>
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    public List<Game> Games { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Genre, GenreVm>()
            .ForMember(g => g.Id,
                o =>
                    o.MapFrom(g => g.Id))
            .ForMember(g => g.Name,
                o =>
                    o.MapFrom(g => g.Name))
            .ForMember(g => g.Games,
                o =>
                    o.MapFrom(g => g.Games));
    }
}