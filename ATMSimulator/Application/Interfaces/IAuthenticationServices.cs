using ATMSimulator.Domain.DTOs;

namespace ATMSimulator.Application.Interfaces;

public interface IAuthenticationServices
{
    public LoggedInDTOs Logged(string cardNumber, int pin);

    public int IncreaseLoginAttempts(string cardNumber);

    public bool InactiveCard(string cardNumber);
}