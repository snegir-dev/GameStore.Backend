using GameStore.Application.Common.Exceptions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GameStore.Application.CQs.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Unit>
{
    private readonly IGameStoreDbContext _context;
    private readonly UserManager<Domain.User> _userManager;
    private readonly SignInManager<Domain.User> _signInManager;

    public CreateUserCommandHandler(IGameStoreDbContext context,
        SignInManager<Domain.User> signInManager,
        UserManager<Domain.User> userManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Domain.User()
        {
            UserName = request.Login,
            Email = request.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Unit.Value;
        }

        foreach (var error in result.Errors)
        {
            throw new UserCreateException(user.UserName, error.Description);
        }

        return Unit.Value;
    }
}