using ATMSimulator.Domain.DTOs;
using ATMSimulator.Infrastructure.Data;
using ATMSimulator.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ATMSimulator.Infrastructure.Repositories;

public class CardRepo : ICardRepo
{
    private readonly ATMContext _db = new();

    public LoggedInDTOs? AuthenticateCard(string cardNum, int pin)
    {
        var card = _db.Cards.FirstOrDefault(c => c.CardNumber == cardNum && c.Password == pin);
        if (card == null) return null;
        card.FailedLoginAttempts = 0;
        _db.SaveChanges();
        return new LoggedInDTOs(card.CardNumber, card.HolderName, card.IsActive);
    }

    public int IncreaseFailedLoginAttempts(string cardNum)
    {
        var card = _db.Cards.FirstOrDefault(c => c.CardNumber == cardNum);
        if (card == null) return 0;
        card.FailedLoginAttempts++;
        _db.SaveChanges();
        return card.FailedLoginAttempts;
    }

    public bool BlockingCard(string cardNum)
    {
        var card = _db.Cards.FirstOrDefault(c => c.CardNumber == cardNum);
        if (card == null) return false;
        if (card.FailedLoginAttempts >= 3)card.IsActive = false;
        _db.SaveChanges();
        return true;
    }

    public bool TransactionAmount(string sourceCardNumber, string destinationCardNumber, float amount)
    {
        var sourceCard = _db.Cards.FirstOrDefault(c => c.CardNumber == sourceCardNumber);
        var destinationCard = _db.Cards.FirstOrDefault(c => c.CardNumber == destinationCardNumber);
        if (sourceCard == null || destinationCard == null) throw new Exception("The destination card is not valid.");
        if (sourceCard.Balance < amount) throw new Exception("Your account does not have sufficient funds.");

        using var transaction = _db.Database.BeginTransaction();
        try
        {
            sourceCard.Balance -= amount;
            destinationCard.Balance += amount;
            _db.SaveChanges();
            transaction.Commit();
            return true;
        }
        catch (Exception)
        {
            transaction.Rollback();
            return false;
        }
    }
}