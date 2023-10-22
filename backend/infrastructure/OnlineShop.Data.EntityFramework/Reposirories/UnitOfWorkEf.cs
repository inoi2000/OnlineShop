using OnlineShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.EntityFramework.Reposirories
{
    public class UnitOfWorkEf : IUnitOfWork
    {
        public IAccountRepository AccountRepository { get; }
        public ICartRepository CartRepository { get; }
        private readonly AppDbContext _dbContext;

        public UnitOfWorkEf(
            AppDbContext dbContext,
            IAccountRepository accountRepository,
            ICartRepository cartRepository)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            CartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        }        

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
