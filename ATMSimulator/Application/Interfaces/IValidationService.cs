namespace ATMSimulator.Application.Interfaces;

public interface IValidationService
{
    public bool CardNumberIsValid(string cardNumber);

    public bool MaximumDailyTransferIsValid(string cardNumber, float amount);
}