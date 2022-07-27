using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using AutoMapper;
using GameStore.Application.CQs.User;
using GameStore.Application.CQs.User.Commands.Registration;
using GameStore.Application.CQs.User.Queries.Login;
using GameStore.Domain;
using GameStore.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/user")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

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