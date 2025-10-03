using ATMSimulator.Application.Interfaces;
using ATMSimulator.Domain.DTOs;
using ATMSimulator.Domain.Entities;
using ATMSimulator.Infrastructure.Interfaces;
using ATMSimulator.Infrastructure.Repositories;

namespace ATMSimulator.Application.Services;

public class TransactionServices : ITransactionServices
{
    private readonly ITransactionRepo _transactionRepo = new TransactionRepo();
    private readonly CardRepo _cardRepo = new();
    private readonly ValidationService _validation = new();
    public bool MakeTransaction(string source, string destination, float amount)
    {
        if (!_validation.CardNumberIsValid(destination)) throw new Exception("The card number must be 16 digits.");
        if (!_validation.MaximumDailyTransferIsValid(source, amount)) throw new Exception("Your maximum daily transfer limit is not sufficient for this transaction.");
        var transactionResult = _cardRepo.TransactionAmount(source, destination, amount);
        _transactionRepo.AddTransaction(new Transaction(source, destination, amount, transactionResult));
        return transactionResult;
    }

    public List<Transaction> GetTransactions(string cardNumber)
    {
        return _transactionRepo.GetAllTransactions(cardNumber);
    }
}