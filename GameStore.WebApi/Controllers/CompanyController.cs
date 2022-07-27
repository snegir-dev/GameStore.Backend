using AutoMapper;
using GameStore.Application.CQs.Company.Commands.Create;
using GameStore.Application.CQs.Company.Commands.Delete;
using GameStore.Application.CQs.Company.Commands.Update;
using GameStore.Application.CQs.Company.Queries.GetCompany;
using GameStore.Application.CQs.Company.Queries.GetListCompany;
using GameStore.Domain;
using GameStore.WebApi.Models.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[Authorize(Roles = "Admin")]
[Route("api/companies")]
public class CompanyController : BaseController
{
    private readonly IMapper _mapper;

    public CompanyController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> Get()
    {
        var query = new GetListCompanyQuery();
        var companies = await Mediator.Send(query);
        
        return Ok(companies.Companies);
    }

    [AllowAnonymous]
    [HttpGet("{id:long}")]
    public async Task<ActionResult<CompanyVm>> Get(long id)
    {
        var query = new GetCompanyQuery()
        {
            Id = id
        };
        var vm = await Mediator.Send(query);

        return Ok(vm);
    }

    [HttpPost]
    public async Task<ActionResult<long>> Create([FromBody] CreateCompanyDto companyDto)
    {
        var command = _mapper.Map<CreateCompanyCommand>(companyDto);
        var companyId = await Mediator.Send(command);

        return Created("api/companies", companyId);
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<Company>> Update(long id, [FromBody] UpdateCompanyDto companyDto)
    {
        var command = _mapper.Map<UpdateCompanyCommand>(companyDto);
        command.Id = id;
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<ActionResult> Delete(long id)
    {
        var command = new DeleteCompanyCommand()
        {
            Id = id
        };
        await Mediator.Send(command);
        
        return NoContent();
    }
}