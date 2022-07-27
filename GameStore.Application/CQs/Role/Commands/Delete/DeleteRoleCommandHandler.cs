using GameStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.Role.Commands.Delete;

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Unit>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public DeleteRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Unit> Handle(DeleteRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null)
            throw new NotFoundException(nameof(IdentityRole<long>), request.Id);

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded)
            throw new RecordCreateException(result.Errors.ToList());
        
        return Unit.Value;
    }
}