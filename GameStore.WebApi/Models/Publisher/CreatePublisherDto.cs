using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Publisher.Commands;
using GameStore.Application.CQs.Publisher.Commands.Create;

namespace GameStore.WebApi.Models.Publisher;

public class CreatePublisherDto : IMapWith<CreatePublisherCommand>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreatePublisherDto, CreatePublisherCommand>()
            .ForMember(p => p.Name,
                o =>
                    o.MapFrom(p => p.Name))
            .ForMember(p => p.Description,
                o =>
                    o.MapFrom(p => p.Description))
            .ForMember(p => p.DateFoundation,
                o =>
                    o.MapFrom(p => p.DateFoundation));
    }
}