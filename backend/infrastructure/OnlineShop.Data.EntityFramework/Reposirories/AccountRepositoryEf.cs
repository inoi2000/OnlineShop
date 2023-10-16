using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Data.EntityFramework.Repositories
{
    public class AccountRepositoryEf : EfRepository<Account>, IAccountRepository
    {
        public AccountRepositoryEf(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Account> GetAccountByLogin(string login, CancellationToken token)
        {
            if (string.IsNullOrEmpty(login)) { throw new ArgumentException($"\"{nameof(login)}\" не может быть неопределенным или пустым.", nameof(login)); }

            return await Entities.SingleAsync(e => e.Login == login, token);
        }

        public async Task<Account?> FindAccountByLogin(string login, CancellationToken token)
        {
            if (string.IsNullOrEmpty(login)) { throw new ArgumentException($"\"{nameof(login)}\" не может быть неопределенным или пустым.", nameof(login)); }

            return await Entities.SingleOrDefaultAsync(e => e.Login == login, token);
        }

        public async Task<Account> GetAccountByEmail(string email, CancellationToken token)
        {
            if (string.IsNullOrEmpty(email)) {  throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email)); }

            return await Entities.SingleAsync(e => e.Email == email, token);
        }

        public async Task<Account?> FindAccountByEmail(string email, CancellationToken token)
        {
            if (string.IsNullOrEmpty(email)) { throw new ArgumentException($"\"{nameof(email)}\" не может быть неопределенным или пустым.", nameof(email)); }

            return await Entities.SingleOrDefaultAsync(e => e.Email == email, token);
        }

        public override async Task Update(Account entity, CancellationToken token)
        {
            await base.Update(entity, token);
        }
    }
}
