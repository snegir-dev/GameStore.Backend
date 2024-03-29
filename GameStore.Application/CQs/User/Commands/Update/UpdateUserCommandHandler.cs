﻿using GameStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.User.Commands.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
{
    private readonly UserManager<Domain.User> _userManager;

    public UpdateUserCommandHandler(UserManager<Domain.User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Unit> Handle(UpdateUserCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id.ToString());
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.Id);

        user.UserName = request.UserName;
        user.Email = request.Email;

        if (!await _userManager.CheckPasswordAsync(user, request.Password))
            throw new Exception("The data is not correct");
        
        await _userManager.UpdateAsync(user);
            
        return Unit.Value;

    }
}