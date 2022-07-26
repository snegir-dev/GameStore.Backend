using AutoMapper;
using GameStore.Application.CQs.Game.Commands.Create;
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
    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateGameCommand game)
    {
        var gameId = await Mediator.Send(game);
        
        return Ok(gameId);
    }
}