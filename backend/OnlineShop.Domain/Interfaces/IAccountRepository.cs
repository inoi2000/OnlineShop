using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Task<Account> GetAccountByLogin(string login, CancellationToken token);
        Task<Account?> FindAccountByLogin(string login, CancellationToken token);
        Task<Account> GetAccountByEmail(string email, CancellationToken token);
        Task<Account?> FindAccountByEmail(string email, CancellationToken token);
    }
}
