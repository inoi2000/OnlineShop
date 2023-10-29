using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.HttpModels.Models
{
    public class CartResponse
    {
        public Guid Id { get; init; }

        public Guid AccountId { get; set; }

        public List<CartItemResponse> Items { get; set; }

        public int ItemCount => Items.Count;

        public CartResponse() {}
    }
}
