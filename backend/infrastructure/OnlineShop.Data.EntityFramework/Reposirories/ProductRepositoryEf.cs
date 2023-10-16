using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using System.Xml.Linq;

namespace OnlineShop.Data.EntityFramework.Repositories
{
    public class ProductRepositoryEf : EfRepository<Product>, IProductRepository
    {
        public ProductRepositoryEf(AppDbContext dbContext) : base(dbContext) { }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(decimal minPrice, decimal maxPrice, CancellationToken token)
        {
            return await Entities.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToListAsync();
        }

        public override async Task Update(Product entity, CancellationToken token)
        {
            var product = await Entities.FirstAsync(p => p.Id == entity.Id, token);

            product.Name = entity.Name;
            product.Description = entity.Description;
            product.Price = entity.Price;
            product.ImagUri = entity.ImagUri;

            await base.Update(product, token);
        }
    }
}
