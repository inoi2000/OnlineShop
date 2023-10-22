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

        public async Task AddItem(Cart cart, Product product, CancellationToken token, double quantity = 1d)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));
            if (product == null) throw new ArgumentNullException(nameof(product));
            if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

            var existedItem = cart.Items.SingleOrDefault(it => it.ProductId == product.Id);
            if (existedItem is not null)
            {
                existedItem.Quantity += quantity;
            }
            else
            {
                cart.Items.Add(new CartItem()
                {
                    ProductId = product.Id,
                    Quantity = quantity,
                    Price = product.Price
                });
            }

            await _uow.CartRepository.Update(cart, token);
            await _uow.SaveChangesAsync(token);
        }
    }
}
