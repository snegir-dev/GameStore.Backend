using GameStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.Role.Commands.SetRole;

public class SetRoleCommandHandler : IRequestHandler<SetRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;
    private readonly UserManager<Domain.User> _userManager;

    public SetRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager, UserManager<Domain.User> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }

    public async Task<Unit> Handle(SetRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
        
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.UserId);
        if (role == null)
            throw new NotFoundException(nameof(IdentityRole<long>), request.UserId);

        var result = await _userManager.AddToRoleAsync(user, role.Name);

        if (!result.Succeeded)
            throw new RecordCreateException(result.Errors.ToList());

        return Unit.Value;
    }
}