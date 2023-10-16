using Microsoft.AspNetCore.Mvc;
using OnlineShop.Domain.Entities;
using OnlineShop.Domain.Interfaces;

namespace OnlineShop.ApiServer.Controllers
{
    [ApiController]
    [Route("catalog")]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public CatalogController([FromServices] IProductRepository productRepository)
        {

            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

        }

        [HttpGet("get_all_products")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetAllProduct(CancellationToken token)
        {
            try
            {
                IReadOnlyList<Product> product = await _productRepository.GetAll(token);
                return Ok(product);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("get_products")]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] decimal minPrice, [FromQuery] decimal maxPrice, CancellationToken token)
        {
            try
            {
                var product = await _productRepository.GetProductsAsync(minPrice, maxPrice, token);
                return Ok(product);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
        }

        [HttpGet("get_product")]
        public async Task<ActionResult<Product>> GetProduct([FromQuery] Guid Id, CancellationToken token)
        {
            try
            {
                var product = await _productRepository.GetById(Id, token);
                return Ok(product);
            }
            catch (InvalidOperationException)
            {
                return NotFound(Id);
            }
        }

        [HttpPost("add_product")]
        public async Task<IResult> AddProduct(Product product, CancellationToken token)
        {
            await _productRepository.Add(product, token);
            return Results.Created($"/products/{product.Id}", product);
        }

        [HttpPut("edit_product")]
        public async Task<IResult> EditProduct(Product product, CancellationToken token)
        {
            try
            {
                await _productRepository.Update(product, token);
                return Results.Ok();
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound(product);
            }

        }
        
        [HttpDelete("delete_product")]
        public async Task<IResult> DeleteProduct([FromQuery] Guid Id, CancellationToken token)
        {
            try
            {
                await _productRepository.Delete(Id, token);
                return Results.Ok();
            }
            catch (InvalidOperationException)
            {
                return Results.NotFound(Id);
            }
        }
    }

}
