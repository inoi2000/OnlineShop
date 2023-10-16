using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetById(Guid id, CancellationToken token);
        Task<IReadOnlyList<TEntity>> GetAll(CancellationToken token);
        Task Add(TEntity entity, CancellationToken token);
        Task Update(TEntity entity, CancellationToken token);
        Task Delete(Guid id, CancellationToken token);
    }
}
