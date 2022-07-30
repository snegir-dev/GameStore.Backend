using AutoMapper;
using GameStore.Application.CQs.Publisher.Commands.Create;
using GameStore.Application.CQs.Publisher.Commands.Delete;
using GameStore.Application.CQs.Publisher.Commands.Update;
using GameStore.Application.CQs.Publisher.Queries.GetListPublisher;
using GameStore.Application.CQs.Publisher.Queries.GetPublisher;
using GameStore.WebApi.Models.Company;
using GameStore.WebApi.Models.Publisher;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/publishers")]
public class PublisherController : BaseController
{
    private readonly IMapper _mapper;

    public PublisherController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var query = new GetListPublisherQuery();
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Publishers);
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<PublisherVm>> Get(long id)
    {
        var query = new GetPublisherQuery()
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreatePublisherDto company)
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