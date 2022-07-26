using AutoMapper;
using GameStore.Application.CQs.Game.Commands.Create;
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

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> Get()
    {
        var query = new GetListGameQuery();
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Games);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateGameCommand game)
    {
        var gameId = await Mediator.Send(game);
        
        return Ok(gameId);
    }
}