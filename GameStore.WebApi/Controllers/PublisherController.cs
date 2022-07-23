using AutoMapper;
using GameStore.Application.CQs.Publisher.Commands;
using GameStore.Application.CQs.Publisher.Commands.Create;
using GameStore.WebApi.Models.Company;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/publishers")]
public class PublisherController : BaseController
{
    private readonly IMapper _mapper;

    public PublisherController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<ActionResult> Create([FromBody] CreateCompanyDto company)
    {
        var command = _mapper.Map<CreatePublisherCommand>(company);
        var companyId = await Mediator.Send(command);

        return Created("api/publishers", companyId);
    }
}