namespace ATMSimulator.Domain.Entities;

public class Card
{
    public string CardNumber { get; private set; }
    public string HolderName { get; set; }
    public float Balance { get; set; }
    public bool IsActive {get; set;}
    public int Password {get; private set;}
    
    public int FailedLoginAttempts { get; set; }

    public ICollection<Transaction> DepositList { get; set; } = new List<Transaction>();
    public ICollection<Transaction> WithdrawList { get; set; } = new List<Transaction>();
    

    public void ChangePassword(int newPassword)
    {
        Password = newPassword;
    }
}