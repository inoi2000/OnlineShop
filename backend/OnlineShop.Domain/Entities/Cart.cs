using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities
{
    public class Cart : IEntity
    {
        public Guid Id { get; init; }
        public Guid AccountId { get; set; }
        public List<CartItem> Items { get; set; }
        public int ItemCount => Items.Count;

        public Cart() {}
        public Cart(Guid AccountId)
        {
            Id = Guid.NewGuid();
            this.AccountId = AccountId;
            Items = new List<CartItem>();
        }

        public void AddItem(Guid productId, double quantity)
        {
            if(quantity<=0) throw new ArgumentOutOfRangeException(nameof(quantity));
            if(Items==null) throw new InvalidOperationException(nameof(Items));

            var existedItem = Items.SingleOrDefault(it => it.ProductId == productId);
            if (existedItem is not null)
            {
                existedItem.Quantity += quantity;
            }
            else
            {
                Items.Add(new CartItem()
                {
                    ProductId = productId,
                    Quantity = quantity
                });
            }
        }
    }
}
