namespace ATMSimulator.Domain.DTOs;

public class LoggedInDTOs(string cardNumber, string holderName, bool isActive)
{
    public string CardNumber { get; set; } = cardNumber;
    public string HolderName { get; set; } = holderName;
    public bool IsActive { get; set; } = isActive;
}