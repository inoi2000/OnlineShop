using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Domain.Entities
{
    public class CartItem : IEntity
    {
        public Guid Id { get; init; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public double Quantity { get; set; }
    }
}
