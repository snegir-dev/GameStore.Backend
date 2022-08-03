using GameStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.Role.Commands.Update;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public UpdateRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(UpdateRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null)
            throw new NotFoundException(nameof(IdentityRole<long>), request.Id);

        var isExistRole = await _roleManager.FindByNameAsync(request.Name) != null;
        if (isExistRole)
            throw new RecordExistsException(nameof(IdentityRole<long>), request.Name);

        role.Name = request.Name;
        role.NormalizedName = request.Name.ToUpper();
        
        await _roleManager.UpdateAsync(role);
        
        return Unit.Value;
    }
}