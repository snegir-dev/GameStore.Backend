using GameStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.EntityTypeConfigurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => c.Id).IsUnique();
        builder.Property(c => c.Name).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Description).IsRequired().HasMaxLength(600);
        builder.Property(c => c.DateFoundation).IsRequired();
        builder.HasMany(c => c.Games)
            .WithOne(g => g.Company);
    }
}