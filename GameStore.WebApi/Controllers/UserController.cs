using System.Reflection;
using GameStore.Application.CQs.User;
using GameStore.Application.CQs.User.Commands.Registration;
using GameStore.Application.CQs.User.Queries.Login;
using GameStore.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

public class UserController : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> LoginAsync([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [HttpPost("registration")]
    public async Task<ActionResult<UserDto>> Registration([FromBody] RegistrationCommand command)
    {
        return await Mediator.Send(command);
    }
}