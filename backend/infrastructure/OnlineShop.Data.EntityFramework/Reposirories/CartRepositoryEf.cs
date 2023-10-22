using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.EntityFramework.Repositories;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Data.EntityFramework.Reposirories
{
    public class CartRepositoryEf : EfRepository<Cart>, ICartRepository
    {
        public CartRepositoryEf(AppDbContext dbContext) : base(dbContext) { }

        public async Task<Cart> GetByAccountId(Guid accountId, CancellationToken token)
        {
            return await Entities.FirstAsync(c => c.AccountId == accountId);
        }
    }
}
