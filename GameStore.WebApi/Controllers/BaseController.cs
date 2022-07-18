using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class BaseController : ControllerBase
{
    private IMediator? _mediator;
    protected IMediator Mediator =>
        _mediator ?? HttpContext.RequestServices.GetService<IMediator>()!;

    internal long? UserId => !User.Identity.IsAuthenticated
        ? null
        : Convert.ToInt64(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
}