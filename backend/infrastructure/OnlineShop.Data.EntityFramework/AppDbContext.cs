using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Data.EntityFramework
{
    public class AppDbContext : DbContext
    {        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected AppDbContext() { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(p => new { p.Name, p.Price});
        }

        public DbSet<Product> Products => Set<Product>();
        public DbSet<Account> Accounts => Set<Account>();
    }

}
