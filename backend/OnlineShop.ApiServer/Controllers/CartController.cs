using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;
using OnlineShop.Domain.Services;
using OnlineShop.HttpModels.Models;
using System.Security.Claims;

namespace OnlineShop.WebApi.Controllers
{
    [ApiController]
    [Route("cart")]
    public class CartController : ControllerBase
    {
        private readonly CartService _cartService;
        private readonly IProductRepository _productRepository;

        public CartController([FromServices] CartService cartService, IProductRepository productRepository)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }

        [Authorize]
        [HttpGet("get_cart")]
        public async Task<ActionResult<CartResponse>> GetCart(CancellationToken cancellationToken)
        {
            var cart = await GetCartCurrentUser(cancellationToken);
            
            var cartItemsResonce = new List<CartItemResponse>();
            foreach (var item in cart.Items)
            {
                var product = await _productRepository.GetById(item.ProductId, cancellationToken);
                var productResponce = new ProductResponse()
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    ImagUri = product.ImagUri
                };
                var cateItemResponce = new CartItemResponse() { Id = item.Id, Product = productResponce };
                cartItemsResonce.Add(cateItemResponce);
            }

            var cartResponce = new CartResponse() 
            { 
                Id = cart.Id, 
                AccountId = cart.AccountId,
                Items = cartItemsResonce
            };

            return Ok(cartResponce);
        }

        [Authorize]
        [HttpPost("add_product")]
        public async Task<IActionResult> AddProduct([FromQuery] Guid productId, CancellationToken cancellationToken)
        {
            await _cartService.AddItem(GetAccountId(), productId, cancellationToken);
            return Ok();
        }

        private async Task<Cart> GetCartCurrentUser(CancellationToken cancellationToken)
        {
            var res = await _cartService.GetCartForAccount(GetAccountId(), cancellationToken);
            return res;
        }

        private Guid GetAccountId() 
        {
            var strId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (strId == null) { throw new NullReferenceException(nameof(strId)); }
            var guid = Guid.Parse(strId);
            return guid;
        }
    }
}
