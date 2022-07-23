using AutoMapper;
using GameStore.Application.CQs.Publisher.Commands.Create;
using GameStore.Application.CQs.Publisher.Commands.Delete;
using GameStore.Application.CQs.Publisher.Commands.Update;
using GameStore.WebApi.Models.Company;
using GameStore.WebApi.Models.Publisher;
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
    public async Task<ActionResult<long>> Create([FromBody] CreateCompanyDto company)
    {
        var command = _mapper.Map<CreatePublisherCommand>(company);
        var companyId = await Mediator.Send(command);

        return Created("api/publishers", companyId);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdatePublisherDto publisher)
    {
        var command = _mapper.Map<UpdatePublisherCommand>(publisher);
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeletePublisherCommand()
        {
            Id = id
        };
        await Mediator.Send(command);
        
        return NoContent();
    }
}