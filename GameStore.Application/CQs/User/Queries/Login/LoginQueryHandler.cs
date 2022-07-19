using System.Net;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.User.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, UserDto>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public LoginQueryHandler(UserManager<Domain.User> userManager, SignInManager<Domain.User> signInManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<UserDto> Handle(LoginQuery request, CancellationToken cancellationToken)
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
            return new UserDto()
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = _jwtGenerator.CreateToken(user)
            };
        }

        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
}