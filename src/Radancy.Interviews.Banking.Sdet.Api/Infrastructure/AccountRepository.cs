using Radancy.Interviews.Examples.Banking.Sdet.Api.Domain;

namespace Radancy.Interviews.Examples.Banking.Sdet.Api.Infrastructure;

// This is a simple In Memory storage. It is done for the purpose of the example
// and does not reflect best practices or patterns in any way.
public sealed class AccountRepository : IAccountRepository
{
    private readonly Dictionary<Guid, Account> _accounts = new();

    public Task AddAccount(Account account)
    {
        _accounts[account.Id] = account;

        return Task.CompletedTask;
    }
    
    public Task DeleteAccount(Guid accountId)
    {
        _accounts.Remove(accountId);

        return Task.CompletedTask;
    }
    
    public Task<Account?> GetAccount(Guid accountId)
    {
        var account = _accounts.GetValueOrDefault(accountId);

        return Task.FromResult(account);
    }

    public Task<IEnumerable<Account>> GetAccounts()
    {
        return Task.FromResult((IEnumerable<Account>)_accounts.Values);
    }

    public Task SaveChanges()
    {
        return Task.CompletedTask;
    }
}