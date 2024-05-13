using Radancy.Interviews.Examples.Banking.Sdet.Api.Domain;

namespace Radancy.Interviews.Examples.Banking.Sdet.Api;

public interface IAccountRepository
{
    Task AddAccount(Account account);
    
    Task DeleteAccount(Guid accountId);
    
    Task<Account?> GetAccount(Guid accountId);
    
    Task<IEnumerable<Account>> GetAccounts();

    Task SaveChanges();
}