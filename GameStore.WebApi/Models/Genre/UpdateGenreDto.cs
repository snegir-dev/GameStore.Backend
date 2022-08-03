using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Genre.Commands.Update;

namespace GameStore.WebApi.Models.Genre;

public class UpdateGenreDto : IMapWith<UpdateGenreCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdateGenreDto, UpdateGenreCommand>()
            .ForMember(g => g.Name,
                o =>
                    o.MapFrom(g => g.Name));
    }
}