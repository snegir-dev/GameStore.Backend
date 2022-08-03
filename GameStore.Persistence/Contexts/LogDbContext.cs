using GameStore.Application.Interfaces;
using GameStore.Domain;
using GameStore.Persistence.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Persistence.Contexts;

public class LogDbContext : DbContext, ILogDbContext
{
    public DbSet<Log> Logs { get; set; }

    public LogDbContext(DbContextOptions<LogDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Log>().ToTable("logs");
        modelBuilder.ApplyConfiguration(new LogConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}