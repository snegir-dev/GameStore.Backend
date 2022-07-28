using GameStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.EntityTypeConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);
        builder.HasIndex(u => u.Id).IsUnique();
        builder.Property(u => u.Balance).IsRequired();
        builder.Property(u => u.Email).IsUnicode().IsRequired();
        builder.Property(u => u.UserName).IsRequired().HasMaxLength(255);
        builder.Property(u => u.PasswordHash).IsRequired();
        builder.Property(u => u.RefreshToken).IsRequired(false);
        builder.Property(u => u.RefreshTokenExpiryTime).IsRequired(false);
        builder.HasMany(u => u.Games)
            .WithMany(g => g.Users);
        builder.HasMany(u => u.Baskets)
            .WithOne(b => b.User);
    }
}