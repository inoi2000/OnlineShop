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
        public decimal GetTotalPrice()
        {
            return Items.Sum(it => it.Price);
        }
    }
}
