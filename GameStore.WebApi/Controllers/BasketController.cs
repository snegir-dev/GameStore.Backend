using GameStore.Application.CQs.Basket.Queries.GetBasket;
using GameStore.Application.CQs.Basket.Queries.GetListBasket;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/baskets")]
public class BasketController : BaseController
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<ActionResult<GetListBasketVm>> Get()
    {
        var query = new GetListBasketQuery()
        {
            UserId = UserId
        };
        var vm = await Mediator.Send(query);
        
        return Ok(vm.Baskets);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<BasketVm>> Get(long id)
    {
        var query = new GetBasketQuery()
        {
            Id = id,
            UserId = UserId
        };
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }
}