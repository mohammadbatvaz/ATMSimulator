namespace ATMSimulator.Domain.Entities;

public class Transaction
{
    private Transaction(){ }
    
    public Transaction(string sourceCardNumber, string destinationCardNumber, float amount, bool isSuccessful)
    {
        SourceCardNumber = sourceCardNumber;
        DestinationCardNumber = destinationCardNumber;
        Amount = amount;
        TransactionDate = DateTime.Now;
        IsSuccessful = isSuccessful;
    }
    
    public int TransactionId { get; set; }
    public string SourceCardNumber { get; set; }
    public string DestinationCardNumber { get; set; }
    public float Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public bool IsSuccessful { get; set; }
    
    public Card SourceCard { get; set; }
    public Card DestinationCard { get; set; }
}
