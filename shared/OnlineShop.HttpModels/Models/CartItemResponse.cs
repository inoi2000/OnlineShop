using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace OnlineShop.HttpModels.Models
{
    public class CartItemResponse
    {
        public Guid Id { get; init; }

        public ProductResponse Product { get; set; }

        public double Quantity { get; set; }
    }
}
