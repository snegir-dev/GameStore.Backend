using GameStore.Application.Common.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Application.CQs.Role.Commands.Create;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, long>
{
    private readonly RoleManager<IdentityRole<long>> _roleManager;

    public CreateRoleCommandHandler(RoleManager<IdentityRole<long>> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<long> Handle(CreateRoleCommand request, 
        CancellationToken cancellationToken)
    {
        var result = await _roleManager.CreateAsync(new IdentityRole<long>(request.Name));

        if (!result.Succeeded) 
            throw new RecordCreateException(result.Errors.ToList());
        
        var role = await _roleManager.FindByNameAsync(request.Name);
        
        return role.Id;
    }
}