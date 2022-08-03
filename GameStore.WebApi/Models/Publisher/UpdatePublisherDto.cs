using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Application.CQs.Publisher.Commands.Update;

namespace GameStore.WebApi.Models.Publisher;

public class UpdatePublisherDto : IMapWith<UpdatePublisherCommand>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UpdatePublisherDto, UpdatePublisherCommand>()
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