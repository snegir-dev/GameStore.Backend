using AutoMapper;
using AutoMapper.QueryableExtensions;
using GameStore.Application.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.CQs.User.Queries.GetListUser;

public class GetListUserQueryHandler : IRequestHandler<GetListUserQuery, GetListUserVm>
{
    private readonly IGameStoreDbContext _context;
    private readonly UserManager<Domain.User> _userManager;
    private readonly IMapper _mapper;

    public GetListUserQueryHandler(IGameStoreDbContext context, IMapper mapper,
        UserManager<Domain.User> userManager)
    {
        _context = context;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<GetListUserVm> Handle(GetListUserQuery request,
        CancellationToken cancellationToken)
    {
        var users = (await _context.Users
                .ToListAsync(cancellationToken))
            .Select(u =>
            {
                var role = _userManager.GetRolesAsync(u).Result;

                var userDto = _mapper.Map<UserDto>(u);
                userDto.Role = string.Join(", ", role);

                return userDto;
            });

        return new GetListUserVm() { Users = users };
    }
}