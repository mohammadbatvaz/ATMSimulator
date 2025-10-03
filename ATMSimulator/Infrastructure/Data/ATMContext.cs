using ATMSimulator.Domain.Entities;
using ATMSimulator.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace ATMSimulator.Infrastructure.Data;

public class ATMContext : DbContext
{
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=MBATVAZ-PC\SQLEXPRESS;Database=ATM_DB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CardConfigurations());
        modelBuilder.ApplyConfiguration(new TransactionConfigurations());
    }
}