using ErrorOr;

namespace Radancy.Interviews.Examples.Banking.Sdet.Api.Domain;

public sealed class Account
{
    public static ErrorOr<Account> Create(decimal initialFunds)
    {
        var result = ValidateFunds(initialFunds).Then(_ => ValidateTransaction(initialFunds, initialFunds));
        
        return result.Match(
            onValue: _ => CreateAccount(initialFunds),
            ErrorOr<Account>.From);
        
        Account CreateAccount(decimal funds)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Funds = funds
            };
        }
    }

    public Guid Id { get; private set; }

    public decimal Funds { get; private set; }

    public ErrorOr<Success> ExecuteTransaction(decimal amount)
    {
        var newFunds = Funds + amount;
        
        var result = ValidateTransaction(amount, Funds).Then(_ => ValidateFunds(newFunds));
        
        return result.Match(
            onValue: _ =>
            {
                Funds = newFunds;

                return Result.Success;
            },
            ErrorOr<Success>.From);
    }

    private static ErrorOr<Success> ValidateFunds(decimal funds)
    {
        if (funds < 100)
        {
            return Error.Validation(description: "Account must have at least 100$ at any moment in time.");
        }

        return Result.Success;
    }
    
    private static ErrorOr<Success> ValidateTransaction(decimal amount, decimal currentFunds)
    {
        if (amount > 10000)
        {
            return Error.Validation(description: "Deposit limit is 10000$.");
        }

        if (amount < 0 && Math.Abs(amount) > currentFunds * (decimal)0.9)
        {
            return Error.Validation(description: "More than 90% of the current funds cannot be withdrawn.");
        }

        return Result.Success;
    } 
}