using AutoMapper;
using GameStore.Application.CQs.Game.Commands.Create;
using GameStore.Application.CQs.Game.Commands.Delete;
using GameStore.Application.CQs.Game.Commands.Purchase;
using GameStore.Application.CQs.Game.Commands.Update;
using GameStore.Application.CQs.Game.Queries.GetGame;
using GameStore.Application.CQs.Game.Queries.GetListGame;
using GameStore.WebApi.Models.Game;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/games")]
public class GameController : BaseController
{
    private readonly IMapper _mapper;

    public GameController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
    [ResponseCache(CacheProfileName = "Caching")]
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> Get()
    {
        var query = new GetListGameQuery();
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Games);
    }
    
    [ResponseCache(CacheProfileName = "Caching")]
    [AllowAnonymous]
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
        
        return Created("api/games", gameId);
    }

    [HttpPost("{id:long}")]
    public async Task<ActionResult> Purchase(long id)
    {
        var command = new PurchaseGameCommand()
        {
            GameId = id,
            UserId = UserId
        };
        var gameId = await Mediator.Send(command);
        
        return Ok(gameId);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateGameDto game)
    {
        var command = _mapper.Map<UpdateGameCommand>(game);
        command.Id = id;
        await Mediator.Send(command);
        
        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var query = new DeleteGameCommand()
        {
            Id = id
        };
        await Mediator.Send(query);
        
        return NoContent();
    }
}