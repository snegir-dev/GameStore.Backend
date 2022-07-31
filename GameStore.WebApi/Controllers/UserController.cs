using AutoMapper;
using GameStore.Application.CQs.User;
using GameStore.Application.CQs.User.Commands.RefreshToken;
using GameStore.Application.CQs.User.Commands.Registration;
using GameStore.Application.CQs.User.Commands.ReplenishmentBalance;
using GameStore.Application.CQs.User.Commands.Update;
using GameStore.Application.CQs.User.Queries.GetListUser;
using GameStore.Application.CQs.User.Queries.GetUser;
using GameStore.Application.CQs.User.Queries.Login;
using GameStore.WebApi.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/users")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var query = new GetListUserQuery();
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Users);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<UserVm>> Get(long id)
    {
        var query = new GetUserQuery()
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserVm>> GetMe()
    {
        var query = new GetUserQuery()
        {
            Id = UserId
        };
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]  
    public async Task<ActionResult<AuthenticatedResponse>> Authenticate([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticatedResponse>> Register([FromBody] RegistrationCommand command)
    {
        return await Mediator.Send(command);
    }
    
    [Authorize]
    [HttpPost("replenishment-balance")]
    public async Task<ActionResult<string>> ReplenishmentBalance(
        [FromBody] ReplenishmentBalanceDto replenishmentBalance)
    {
        var command = _mapper.Map<ReplenishmentBalanceCommand>(replenishmentBalance);
        command.UserId = UserId;
        var response = await Mediator.Send(command);
        
        return Ok(response);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult> Update(UpdateUserDto user)
    {
        var command = _mapper.Map<UpdateUserCommand>(user);
        command.Id = UserId;
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<ActionResult<AuthenticatedResponse>> RefreshToken(
        [FromBody] RefreshTokenCommand token)
    {
        return await Mediator.Send(token);
    }
}