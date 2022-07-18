using System.Net;
using GameStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.User.Queries.Login;

public class LoginHandler : IRequestHandler<LoginQuery, Domain.User>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public LoginHandler(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Domain.User> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            throw new NotFoundException(nameof(Domain.User), request.Email);
        }

        var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Password, false);

        if (result.Succeeded)
        {
            return new Domain.User()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = "test"
            };
        }

        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
}