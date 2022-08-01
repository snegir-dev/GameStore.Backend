﻿using AutoMapper;
using GameStore.Application.CQs.Basket.Commands.Create;
using GameStore.Application.CQs.Basket.Commands.Delete;
using GameStore.Application.CQs.Basket.Commands.Update;
using GameStore.Application.CQs.Basket.Queries.GetBasket;
using GameStore.Application.CQs.Basket.Queries.GetListBasket;
using GameStore.WebApi.Models.Basket;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Route("api/baskets")]
[Authorize]
public class BasketController : BaseController
{
    private readonly IMapper _mapper;

    public BasketController(IMapper mapper)
    {
        _mapper = mapper;
    }
    
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
    
    [ResponseCache(CacheProfileName = "Caching")]
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
    
    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateBasketDto basket)
    {
        var command = _mapper.Map<CreateBasketCommand>(basket);
        command.UserId = UserId;
        var basketId = await Mediator.Send(command);
        
        return Ok(basketId);
    }
    
    [HttpPut("{id:long}")]
    public async Task<ActionResult> Update(long id, [FromBody] UpdateBasketDto basket)
    {
        var command = _mapper.Map<UpdateBasketCommand>(basket);
        command.Id = id;
        command.UserId = UserId;
        await Mediator.Send(command);
        
        return NoContent();
    }
    
    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteBasketCommand()
        {
            Id = id,
            UserId = UserId
        };
        await Mediator.Send(command);
        
        return NoContent();
    }
}