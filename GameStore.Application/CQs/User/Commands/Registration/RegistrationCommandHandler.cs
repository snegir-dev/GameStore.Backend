using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.User.Commands.Registration;

public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthenticatedResponse>
{
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;
    private readonly IGameStoreDbContext _context;
    private readonly IJwtGenerator _jwtGenerator;

    public RegistrationCommandHandler(UserManager<Domain.User> userManager,
        IJwtGenerator jwtGenerator, IGameStoreDbContext context, 
        SignInManager<Domain.User> signInManager)
    {
        _userManager = userManager;
        _jwtGenerator = jwtGenerator;
        _context = context;
        _signInManager = signInManager;
    }

    public async Task<AuthenticatedResponse> Handle(RegistrationCommand request,
        CancellationToken cancellationToken)
    {
        if (await _context.Users
                .AnyAsync(u => u.Email == request.Email, cancellationToken))
        {
            throw new RecordCreateException("Email already exist");
        }

        if (await _context.Users
                .AnyAsync(u => u.UserName == request.UserName, cancellationToken))
        {
            throw new RecordCreateException("UserName already exist");
        }

        var refreshToken = _jwtGenerator.CreateRefreshToken();

        var user = new Domain.User()
        {
            UserName = request.UserName,
            Email = request.Email,
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7)
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (result.Succeeded)
        {
            return new AuthenticatedResponse()
            {
                Token = _jwtGenerator.CreateToken(user),
                RefreshToken = refreshToken
            };
        }
   
        throw new RecordCreateException(result.Errors.ToList());
    }
}