using GameStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.EntityTypeConfigurations;

public class GameConfiguration : IEntityTypeConfiguration<Game>
{
    public void Configure(EntityTypeBuilder<Game> builder)
    {
        builder.HasKey(g => g.Id);
        builder.HasIndex(g => g.Id).IsUnique();
        builder.Property(g => g.Name).IsRequired().HasMaxLength(255);
        builder.Property(g => g.Title).IsRequired().HasMaxLength(255);
        builder.Property(g => g.Description).IsRequired().HasMaxLength(600);
        builder.HasOne(g => g.Company)
            .WithMany(c => c.Games);
        builder.HasOne(g => g.Publisher)
            .WithMany(p => p.Games);
        builder.HasMany(g => g.Genres)
            .WithMany(g => g.Games);
        builder.HasMany(g => g.Users)
            .WithMany(u => u.Games);
    }
}