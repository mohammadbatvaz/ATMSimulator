using ATMSimulator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATMSimulator.Infrastructure.Configurations;

public class CardConfigurations : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasKey(c => c.CardNumber);
        builder.HasMany(c => c.DepositList).WithOne(t => t.SourceCard).HasForeignKey(t => t.SourceCardNumber).OnDelete(DeleteBehavior.Restrict);
        builder.HasMany(c => c.WithdrawList).WithOne(t => t.DestinationCard).HasForeignKey(t => t.DestinationCardNumber).OnDelete(DeleteBehavior.Restrict);
        builder.Property(c => c.CardNumber).IsRequired().HasMaxLength(16);
        builder.Property(c => c.HolderName).IsRequired().HasMaxLength(255);
        builder.Property(c => c.Balance).IsRequired().HasDefaultValue(0);
        builder.Property(c => c.IsActive).IsRequired().HasDefaultValue(true);
        builder.Property(c => c.Password).IsRequired().HasMaxLength(4);
        builder.Property(c => c.FailedLoginAttempts).IsRequired().HasDefaultValue(0);
    }
}