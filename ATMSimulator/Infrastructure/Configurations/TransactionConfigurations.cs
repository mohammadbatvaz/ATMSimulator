using ATMSimulator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ATMSimulator.Infrastructure.Configurations;

public class TransactionConfigurations : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(t => t.TransactionId);
        builder.HasOne(t => t.SourceCard).WithMany(c => c.DepositList).HasForeignKey(t => t.SourceCardNumber).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(t => t.DestinationCard).WithMany(c => c.WithdrawList).HasForeignKey(t => t.DestinationCardNumber).OnDelete(DeleteBehavior.Restrict);
        builder.Property(t => t.SourceCardNumber).IsRequired().HasMaxLength(16);
        builder.Property(t => t.DestinationCardNumber).IsRequired().HasMaxLength(16);
        builder.Property(t => t.Amount).IsRequired();
        builder.Property(t => t.TransactionDate).IsRequired();
        builder.Property(t => t.IsSuccessful).IsRequired().HasDefaultValue(false);
    }
}