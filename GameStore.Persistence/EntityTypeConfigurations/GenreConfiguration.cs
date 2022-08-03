using GameStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.EntityTypeConfigurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);
        builder.HasIndex(g => g.Id).IsUnique();
        builder.Property(g => g.Name).IsRequired().HasMaxLength(255);
        builder.HasMany(g => g.Games)
            .WithMany(g => g.Genres);
    }
}