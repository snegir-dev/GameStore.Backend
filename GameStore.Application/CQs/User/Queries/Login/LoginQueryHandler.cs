using System.Net;
using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.User.Queries.Login;

public class LoginQueryHandler : IRequestHandler<LoginQuery, AuthenticatedResponse>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;
    private readonly IMapper _mapper;

    public LoginQueryHandler(UserManager<Domain.User> userManager, 
        SignInManager<Domain.User> signInManager,
        IJwtGenerator jwtGenerator, IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
        _mapper = mapper;
    }

    public async Task<AuthenticatedResponse> Handle(LoginQuery request, CancellationToken cancellationToken)
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
            var refreshToken = _jwtGenerator.CreateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            
            await _userManager.UpdateAsync(user);

            var authenticatedResponse = _mapper.Map<AuthenticatedResponse>(user);
            authenticatedResponse.Token = _jwtGenerator.CreateToken(user);

            return authenticatedResponse;
        }

        throw new Exception(HttpStatusCode.Unauthorized.ToString());
    }
}