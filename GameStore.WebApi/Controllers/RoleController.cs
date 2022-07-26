using GameStore.Application.CQs.Role.Commands.Create;
using GameStore.Application.CQs.Role.Commands.Delete;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/roles")]
public class RoleController : BaseController
{
    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateRoleCommand role)
    {
        var roleId = await Mediator.Send(role);
        
        return Created("api/roles", roleId);
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