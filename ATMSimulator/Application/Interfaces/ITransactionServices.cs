
using ATMSimulator.Domain.Entities;

namespace ATMSimulator.Application.Interfaces;

public interface ITransactionServices
{
    public bool MakeTransaction(string source, string destination, float amount);

    public List<Transaction> GetTransactions(string cardNumber);
}