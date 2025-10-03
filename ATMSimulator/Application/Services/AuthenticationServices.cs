using ATMSimulator.Application.Interfaces;
using ATMSimulator.Domain.DTOs;
using ATMSimulator.Exceptions.BusinessException;
using ATMSimulator.Infrastructure.Configurations;
using ATMSimulator.Infrastructure.Interfaces;
using ATMSimulator.Infrastructure.Repositories;

namespace ATMSimulator.Application.Services;

public class AuthenticationServices : IAuthenticationServices
{
    private readonly ICardRepo _cardRepo = new CardRepo();
    private readonly IValidationService _validation = new ValidationService();
    
    public LoggedInDTOs Logged(string cardNumber, int pin)
    {
        if(!_validation.CardNumberIsValid(cardNumber)) throw new Exception("The card number must be 16 digits.");
        return _cardRepo.AuthenticateCard(cardNumber, pin) ?? throw new InvalidCardOrPinException(cardNumber);
    }
    
    public int IncreaseLoginAttempts(string cardNumber)
    {
        var result = _cardRepo.IncreaseFailedLoginAttempts(cardNumber);
        if (result >= 3)
            InactiveCard(cardNumber);
        return result;
    }

    public bool InactiveCard(string cardNumber)
    {
        return _cardRepo.BlockingCard(cardNumber);
    }
}