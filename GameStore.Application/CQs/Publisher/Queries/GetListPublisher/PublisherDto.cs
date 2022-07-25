using AutoMapper;
using GameStore.Application.Common.Mappings;
using GameStore.Domain;

namespace GameStore.Application.CQs.Publisher.Queries.GetListPublisher;

public class PublisherDto : IMapWith<Domain.Publisher>
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateOnly DateFoundation { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Publisher, PublisherDto>()
            .ForMember(p => p.Id,
                o =>
                    o.MapFrom(p => p.Id))
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