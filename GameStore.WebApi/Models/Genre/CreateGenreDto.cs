using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Genre.Commands.Create;

namespace GameStore.WebApi.Models.Genre;

public class CreateGenreDto : IMapWith<CreateGenreCommand>
{
    public string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateGenreDto, CreateGenreCommand>()
            .ForMember(g => g.Name,
                o =>
                    o.MapFrom(g => g.Name));
    }
}