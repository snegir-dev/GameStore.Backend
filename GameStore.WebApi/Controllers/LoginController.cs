using GameStore.Application.CQs.User.Queries.Login;
using GameStore.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.WebApi.Controllers;

public class LoginController : BaseController
{
    [HttpPost("login")]
    public async Task<ActionResult<User>> LoginAsync(string email, string password)
    {
        var user = new LoginQuery()
        {
            Email = email,
            Password = password
        };
        
        return await Mediator.Send(user);
    }
}