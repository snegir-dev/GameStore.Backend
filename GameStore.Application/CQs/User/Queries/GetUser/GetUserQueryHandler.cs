using AutoMapper;
using GameStore.Application.Common.Exceptions;
using GameStore.Application.CQs.User.Queries.GetListUser;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.User.Queries.GetUser;

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, UserVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly IMapper _mapper;
    private readonly UserManager<Domain.User> _userManager;

    public GetUserQueryHandler(IGameStoreDbContext context, UserManager<Domain.User> userManager, 
        IMapper mapper)
    {
        _context = context;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<UserVm> Handle(GetUserQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Baskets)
            .ThenInclude(b => b.Game)
            .Include(u => u.Games)
            .FirstOrDefaultAsync(u => u.Id == request.Id, cancellationToken);
        if (user == null)
            throw new NotFoundException(nameof(Domain.User), request.Id);

        var roles = await _userManager.GetRolesAsync(user);

        var userDto = _mapper.Map<UserVm>(user);
        userDto.Role = string.Join(", ", roles);
        
        userDto.Baskets.ForEach(b => b.User = null!);
        userDto.Games.ForEach(g =>
        {
            g.Baskets = null!;
            g.Users = null!;
        });

        return userDto;
    }
}