using ATMSimulator.Domain.DTOs;

namespace ATMSimulator.Infrastructure.Interfaces;

public interface ICardRepo
{
    public LoggedInDTOs? AuthenticateCard(string cardNum, int pin);

    public int IncreaseFailedLoginAttempts(string cardNum);

    public bool BlockingCard(string cardNum);

    public bool TransactionAmount(string sourceCardNumber, string destinationCardNumber, float amount);
}