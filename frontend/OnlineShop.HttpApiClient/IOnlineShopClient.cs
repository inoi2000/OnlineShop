using OnlineShop.HttpModels.Models;
using OnlineShop.HttpModels.Requests;
using OnlineShop.HttpModels.Responses;

namespace OnlineShopHttpApiClient
{
    public interface IOnlineShopClient
    {
        Task<IReadOnlyList<ProductResponse>> GetAllProductsAsync(CancellationToken token);
        Task<IReadOnlyList<ProductResponse>> GetProductsAsync(decimal minPrice, decimal maxPrice, CancellationToken token);
        Task<ProductResponse> GetProductAsync(Guid id, CancellationToken token);
        Task AddProductAsync(ProductResponse product, CancellationToken token);
        Task EditProductAsync(ProductResponse product, CancellationToken token);
        Task DeleteProductAsync(Guid id, CancellationToken token);


        Task RegisterAccountAsync(RegisterRequest request, CancellationToken token);
        Task<AuthorisationResponse> AuthorisationAsync(AuthorisationRequest request, CancellationToken token);
        void SetAuthorizationToken(string token);
        void DeleteAuthorizationToken();
        Task<AccountResponse> GetCurrentUser(CancellationToken token);
        Task<CartResponse> GetCartAsync(CancellationToken token);
        Task AddProductToCartAsync(Guid productId, CancellationToken token);
    }
}