using AutoMapper;
using GameStore.Application.CQs.Game.Commands.Create;
using GameStore.Application.CQs.Game.Queries.GetGame;
using GameStore.Application.CQs.Game.Queries.GetListGame;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/games")]
public class GameController : BaseController
{
    private readonly IMapper _mapper;

    public GameController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> Get()
    {
        var query = new GetListGameQuery();
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Games);
    }
    
    [HttpGet("{id:long}")]
    public async Task<ActionResult<GameVm>> Get(long id)
    {
        var query = new GetGameQuery()
        {
            Id = id
        };
        var vm = await Mediator.Send(query);
        
        return Ok(vm);
    }
    
    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateGameCommand game)
    {
        var gameId = await Mediator.Send(game);
        
        return Ok(gameId);
    }
}