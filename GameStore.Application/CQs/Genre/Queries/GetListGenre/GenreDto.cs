using AutoMapper;
using GameStore.Application.Common.Mappings;

namespace GameStore.Application.CQs.Genre.Queries.GetListGenre;

public class GenreDto : IMapWith<Domain.Genre>
{
    public long Id { get; set; }
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Genre, GenreDto>()
            .ForMember(g => g.Id,
                o =>
                    o.MapFrom(g => g.Id))
            .ForMember(g => g.Name,
                o =>
                    o.MapFrom(g => g.Name));
    }
}