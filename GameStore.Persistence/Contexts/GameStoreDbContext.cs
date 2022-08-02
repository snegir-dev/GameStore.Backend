﻿using GameStore.Application.Interfaces;
using GameStore.Domain;
using GameStore.Persistence.EntityTypeConfigurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Persistence.Contexts;

public class GameStoreDbContext : IdentityDbContext<User, IdentityRole<long>, long>, 
    IGameStoreDbContext
{
    public DbSet<Game> Games { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<Genre> Genres { get; set; }

    public GameStoreDbContext(DbContextOptions<GameStoreDbContext> options) 
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new GameConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new PublisherConfiguration());
        modelBuilder.ApplyConfiguration(new GenreConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}