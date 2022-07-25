using GameStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.EntityTypeConfigurations;

public class BasketConfiguration : IEntityTypeConfiguration<Basket>
{
    public void Configure(EntityTypeBuilder<Basket> builder)
    {
        builder.HasKey(b => b.Id);
        builder.HasIndex(b => b.Id).IsUnique();
        builder.HasOne(b => b.User)
            .WithMany(u => u.Baskets);
        builder.HasMany(b => b.Games)
            .WithMany(g => g.Baskets);
    }
}