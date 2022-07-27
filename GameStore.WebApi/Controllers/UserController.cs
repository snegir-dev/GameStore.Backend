using AutoMapper;
using GameStore.Application.CQs.User;
using GameStore.Application.CQs.User.Commands.Registration;
using GameStore.Application.CQs.User.Queries.GetListUser;
using GameStore.Application.CQs.User.Queries.GetUser;
using GameStore.Application.CQs.User.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/users")]
public class UserController : BaseController
{
    private readonly IMapper _mapper;

    public UserController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var query = new GetListUserQuery();
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Users);
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult> Get(long id)
    {
        var query = new GetUserQuery()
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public async Task<ActionResult<UserToken>> Authenticate([FromBody] LoginQuery query)
    {
        return await Mediator.Send(query);
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<UserToken>> Register([FromBody] RegistrationCommand command)
    {
        return await Mediator.Send(command);
    }
}