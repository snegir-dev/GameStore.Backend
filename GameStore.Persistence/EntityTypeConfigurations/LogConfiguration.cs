

using GameStore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameStore.Persistence.EntityTypeConfigurations;

public class LogConfiguration : IEntityTypeConfiguration<Log>
{
    public void Configure(EntityTypeBuilder<Log> builder)
    {
        builder.HasKey(l => l.Id);
        builder.HasIndex(l => l.Id)
            .IsUnique();
        builder.Property(l => l.Id)
            .HasColumnName("id");
        builder.Property(l => l.Application)
            .IsRequired(false)
            .HasMaxLength(255)
            .HasColumnName("application");
        builder.Property(l => l.Level)
            .IsRequired(false)
            .HasMaxLength(100)
            .HasColumnName("level");
        builder.Property(l => l.Exception)
            .IsRequired(false)
            .HasMaxLength(8000)
            .HasColumnName("exception");
        builder.Property(l => l.Logged)
            .IsRequired(false)
            .HasColumnName("logged");
        builder.Property(l => l.Logger)
            .IsRequired(false)
            .HasMaxLength(8000)
            .HasColumnName("logger");
        builder.Property(l => l.Message)
            .IsRequired(false)
            .HasMaxLength(8000)
            .HasColumnName("message");
        builder.Property(l => l.CallSite)
            .IsRequired(false)
            .HasMaxLength(8000)
            .HasColumnName("callSite");
    }
}