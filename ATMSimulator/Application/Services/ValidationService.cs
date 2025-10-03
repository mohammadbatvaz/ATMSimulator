using ATMSimulator.Application.Interfaces;
using ATMSimulator.Infrastructure.Interfaces;
using ATMSimulator.Infrastructure.Repositories;

namespace ATMSimulator.Application.Services;

public class ValidationService : IValidationService
{
    private readonly ITransactionRepo _transactionRepo = new TransactionRepo();
    public bool CardNumberIsValid(string cardNumber)
    {
        return cardNumber.Length == 16;
    }
    
    public bool MaximumDailyTransferIsValid(string cardNumber, float amount)
    {
        return (amount + _transactionRepo.GetDailyTransactionAmount(cardNumber)) <= 250;
    }
}