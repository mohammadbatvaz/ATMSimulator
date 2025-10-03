namespace ATMSimulator.Exceptions.BusinessException;

public class InvalidCardOrPinException(string cardNumber) : Exception
{
    public string CardNumber { get; } = cardNumber;
};