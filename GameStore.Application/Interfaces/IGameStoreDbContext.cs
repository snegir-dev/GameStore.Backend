using GameStore.Domain;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Application.Interfaces;

public interface IGameStoreDbContext
{
    DbSet<Game> Games { get; set; }
    DbSet<Publisher> Publishers { get; set; }
    DbSet<Company> Companies { get; set; }
    DbSet<Genre> Genres { get; set; }
    DbSet<User> Users { get; set; }

    Task<int> SaveChangesAsync(CancellationToken token);
}