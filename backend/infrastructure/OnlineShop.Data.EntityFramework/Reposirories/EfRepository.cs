using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.Data.EntityFramework.Repositories
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly AppDbContext _dbContext;
        public EfRepository(AppDbContext dbContext)
        {
            if (dbContext is null) { throw new ArgumentNullException(nameof(dbContext)); }
            _dbContext = dbContext;
        }

        protected DbSet<TEntity> Entities => _dbContext.Set<TEntity>();

        public virtual Task<TEntity> GetById(Guid Id, CancellationToken token)
            => Entities.FirstAsync(it => it.Id == Id, token);

        public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken token)
            => await Entities.ToListAsync(token);

        public virtual async Task Add(TEntity entity, CancellationToken token)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            await Entities.AddAsync(entity, token);
            await _dbContext.SaveChangesAsync(token);
        }

        public virtual async Task AddUnsafe(TEntity entity, CancellationToken token)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            await Entities.AddAsync(entity, token);
        }

        public virtual async Task Update(TEntity entity, CancellationToken token)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(token);
        }

        public virtual async Task UpdateUnsafe(TEntity entity, CancellationToken token)
        {
            if (entity is null) { throw new ArgumentNullException(nameof(entity)); }

            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual async Task Delete(Guid id, CancellationToken token)
        {
            Entities.Remove(await GetById(id, token));
            await _dbContext.SaveChangesAsync(token);
        }

        public virtual async Task DeleteUnsafe(Guid id, CancellationToken token)
        {
            Entities.Remove(await GetById(id, token));
        }
    }
}
