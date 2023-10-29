using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Services
{
    public class CartService
    {
        private readonly IUnitOfWork _uow;

        public CartService(IUnitOfWork uow)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        public async Task<Cart> GetCartForAccount(Guid accountId, CancellationToken token)
        {
            var cart = await _uow.CartRepository.GetByAccountId(accountId, token);
            return cart;
        }

        public async Task AddItem(Guid accountId, Product product, CancellationToken token, double quantity = 1d)
        {
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            var cart = await _uow.CartRepository.GetByAccountId(accountId, token);
            await AddItem(cart, product, token, quantity);
        }

        public async Task AddItem(Guid accountId, Guid productId, CancellationToken token, double quantity = 1d)
        {
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));
            var cart = await _uow.CartRepository.GetByAccountId(accountId, token);
            await AddItem(cart, productId, token, quantity);
        }

        public async Task AddItem(Cart cart, Product product, CancellationToken token, double quantity = 1d)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            cart.AddItem(product.Id, quantity);

            await _uow.CartRepository.Update(cart, token);
            await _uow.SaveChangesAsync(token);
        }

        public async Task AddItem(Cart cart, Guid productId, CancellationToken token, double quantity = 1d)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            cart.AddItem(productId, quantity);

            await _uow.CartRepository.Update(cart, token);
            await _uow.SaveChangesAsync(token);
        }
    }
}
