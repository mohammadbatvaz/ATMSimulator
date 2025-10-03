
using ATMSimulator.Domain.Entities;
using ATMSimulator.Infrastructure.Data;
using ATMSimulator.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMSimulator.Infrastructure.Repositories;

public class TransactionRepo : ITransactionRepo
{
    private readonly ATMContext _db = new();
    
    public float GetDailyTransactionAmount(string cardNumber)
    {
        var totalToday = _db.Transactions
            .Where(t => t.SourceCardNumber == cardNumber && t.TransactionDate == DateTime.Today)
            .Sum(t => t.Amount);
        return totalToday;
    }

    public bool AddTransaction(Transaction t)
    {
        _db.Transactions.Add(t);
        return _db.SaveChanges() > 0;
    }

    public List<Transaction> GetAllTransactions(string cardNumber)
    {
        return _db.Transactions.AsNoTracking()
            .Where(t => t.SourceCardNumber == cardNumber)
            .ToList();
    }
}