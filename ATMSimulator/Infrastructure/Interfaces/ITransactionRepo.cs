using ATMSimulator.Domain.Entities;

namespace ATMSimulator.Infrastructure.Interfaces;

public interface ITransactionRepo
{
    public float GetDailyTransactionAmount(string cardNumber);

    public bool AddTransaction(Transaction t);

    public List<Transaction> GetAllTransactions(string cardNumber);
}