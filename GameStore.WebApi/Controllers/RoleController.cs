using AutoMapper;
using GameStore.Application.CQs.Role.Commands.Create;
using GameStore.Application.CQs.Role.Commands.Delete;
using GameStore.Application.CQs.Role.Commands.SetRole;
using GameStore.Application.CQs.Role.Commands.Update;
using GameStore.Application.CQs.Role.Queries.GetListRole;
using GameStore.Application.CQs.Role.Queries.GetRole;
using GameStore.WebApi.Models.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/roles")]
public class RoleController : BaseController
{
    private readonly IMapper _mapper;

    public RoleController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
    {
        var query = new GetListRoleQuery();
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Roles);
    }
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult> Get(long id)
    {
        var query = new GetRoleQuery()
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateRoleCommand role)
    {
        var roleId = await Mediator.Send(role);
        
        return Created("api/roles", roleId);
    }
    
    [HttpPost("set-role")]
    public async Task<ActionResult> SetRole([FromBody] SetRoleDto role)
    {
        var command = _mapper.Map<SetRoleCommand>(role);
        command.CurrentUserId = UserId;
        await Mediator.Send(command);
        
        return NoContent();
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateRoleDto role)
    {
        var command = _mapper.Map<UpdateRoleCommand>(role);
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteRoleCommand()
        {
            Id = id
        };
        await Mediator.Send(command);
        
        return NoContent();
    }
}