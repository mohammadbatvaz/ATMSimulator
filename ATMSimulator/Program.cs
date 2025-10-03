using System.Globalization;
using ATMSimulator.Application.Interfaces;
using ATMSimulator.Application.Services;
using ATMSimulator.Domain.DTOs;
using ATMSimulator.Exceptions.BusinessException;
using Spectre.Console;

LoggedInDTOs? user = null;
IAuthenticationServices authService = new AuthenticationServices();
ITransactionServices transactionServices = new TransactionServices();

while (true)
{
    Console.Clear();

    if (user == null)
    {
        user = Login();
        continue;
    }
    if (!user.IsActive)
    {
        AnsiConsole.MarkupLine("[red]Your card is blocked. Please contact the bank.[/]");
        Console.ReadKey(intercept: true);
        user = null;
        continue;
    }
    AnsiConsole.MarkupLine($"[Green] Welcome {user.HolderName}[/]");
    Console.WriteLine("1- Transfer Money");
    Console.WriteLine("2- Show Transactions");
    Console.WriteLine("q- Exit");
    var choice = Console.ReadKey(intercept: true).KeyChar;
    switch (choice)
    {
        case '1':
            TransactionMoney(); break;
        case '2':
            ShowTransactions(); break;
        case 'q':
            return;
        default:
            AnsiConsole.MarkupLine("[red]Invalid choice. Please try again.[/]");
            break;
    }
    Console.ReadKey(intercept: true);
}

LoggedInDTOs? Login()
{
    try
    {
        var cardNum = AnsiConsole.Ask<string>("[green]Enter your card number:[/]");
        var pin = AnsiConsole.Ask<int>("[green]Enter your PIN:[/]");
        return authService.Logged(cardNum, pin);
    }
    catch (InvalidCardOrPinException ex)
    {
        var loginAttempts = authService.IncreaseLoginAttempts(ex.CardNumber);
        AnsiConsole.MarkupLine($"[red]Invalid card number or PIN ({loginAttempts}/3). Please try again.[/]");
        if (loginAttempts >= 3)
            AnsiConsole.MarkupLine("[red]Your card has been blocked due to multiple failed login attempts.[/]");
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]{ex.Message} Please try again.[/]");
    }

    Console.ReadKey(intercept: true);
    return null;
}

void TransactionMoney()
{
    AnsiConsole.MarkupLine($"Source CardNumber is: {user.CardNumber}");
    var destinationCardNumber = AnsiConsole.Ask<string>("[yellow]Enter destination card number:[/]");
    var transferAmount = AnsiConsole.Ask<float>("[yellow]Enter the amount to be transferred:[/]");
    try
    {
        AnsiConsole.MarkupLine(
            transactionServices.MakeTransaction(user.CardNumber, destinationCardNumber, transferAmount)
                ? "[green]Transaction successful.[/]"
                : throw new Exception("[red]Transaction failed.[/]"));
    }
    catch (Exception ex)
    {
        AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
    }
}

void ShowTransactions()
{
    var transactionsList = transactionServices.GetTransactions(user.CardNumber);
    var table = new Table();
    string[] properties = ["ID", "Source Card Number", "Destination Card Number", "Amount", "TransactionDate", "IsSuccessful"];
    
    foreach (var prop in properties)
        table.AddColumn(prop);

    foreach (var item in transactionsList)
    {
        table.AddRow(
            item.TransactionId.ToString(),
            item.SourceCardNumber,
            item.DestinationCardNumber,
            item.Amount.ToString(CultureInfo.InvariantCulture),
            item.TransactionDate.ToString(CultureInfo.InvariantCulture),
            item.IsSuccessful.ToString()
        );
    }
    AnsiConsole.Write(table);
}
